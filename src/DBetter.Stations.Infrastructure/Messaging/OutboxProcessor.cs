using CleanMessageBus.Abstractions;
using DBetter.Stations.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DBetter.Stations.Infrastructure.Messaging;

public class OutboxProcessor(
    IServiceScopeFactory scopeFactory,
    IEventPublisher publisher,
    IOptions<OutboxOptions> outboxOptions,
    TimeProvider time,
    ILogger<OutboxProcessor> logger): BackgroundService
{
    private readonly OutboxOptions _options = outboxOptions.Value;
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(_options.PollingInterval, time);
        do
        {
            try
            {
                // Keep going while batches are full, so a backlog does not wait for the next tick.
                while (await PublishBatchAsync(stoppingToken))
                {
                }
 
                await DeleteProcessedAsync(stoppingToken);
            }
            catch (Exception exception) when (!stoppingToken.IsCancellationRequested)
            {
                logger.LogError(exception, "Processing the outbox failed.");
            }
        }
        while (await timer.WaitForNextTickAsync(stoppingToken));
    }

    private async Task<bool> PublishBatchAsync(CancellationToken cancellationToken)
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<StationsContext>();

        await using var transaction = await db.Database.BeginTransactionAsync(cancellationToken);
        
        var messages = await db.OutboxMessages
            .Where(m => m.ProcessedAt == null && m.FailedAt == null)
            .OrderBy(m => m.OccurredAt)
            .Take(_options.BatchSize)
            .ToListAsync(cancellationToken);
        
        var allPublished = true;
        foreach (var message in messages)
        {
            if (!message.TryRestoreEvent(out var @event, out var error))
            {
                // Retrying cannot fix this: park the entry and continue with the next one.
                logger.LogError("Outbox entry {OutboxMessageId} cannot be restored: {Error}", message.Id, error);
                message.MarkFailed(time.GetUtcNow(), error);
                continue;
            }
 
            try
            {
                await PublishAsync(@event, message.ToPublishOptions(), cancellationToken);
                message.MarkProcessed(time.GetUtcNow());
            }
            catch (Exception exception)
            {
                // Broker unreachable or shutdown: keep the rest (and its order) for the next run.
                if (!cancellationToken.IsCancellationRequested)
                {
                    logger.LogWarning(exception, "Publishing outbox entry {OutboxMessageId} failed.", message.Id);
                    message.RecordFailure(exception.Message);
                }
 
                allPublished = false;
                break;
            }
        }
 
        // Not cancellable: published entries must be marked, otherwise they are published again.
        await db.SaveChangesAsync(CancellationToken.None);
        await transaction.CommitAsync(CancellationToken.None);
 
        return allPublished && messages.Count == _options.BatchSize;
    }
    
    private async Task DeleteProcessedAsync(CancellationToken cancellationToken)
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<StationsContext>();
 
        var threshold = time.GetUtcNow() - _options.Retention;
        await db.OutboxMessages
            .Where(m => m.ProcessedAt < threshold)
            .ExecuteDeleteAsync(cancellationToken);
    }
    
    private ValueTask PublishAsync(object @event, PublishOptions options, CancellationToken cancellationToken) => @event switch
    {
        IDomainEvent domainEvent => publisher.PublishDomainEventAsync(domainEvent, options, cancellationToken),
        IIntegrationEvent integrationEvent => publisher.PublishIntegrationEventAsync(integrationEvent, options, cancellationToken),
        _ => throw new InvalidOperationException($"{@event.GetType()} is neither a domain nor an integration event."),
    };
}