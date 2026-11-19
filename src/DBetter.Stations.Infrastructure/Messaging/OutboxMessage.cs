using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using CleanMessageBus.Abstractions;

namespace DBetter.Stations.Infrastructure.Messaging;

/// <summary>
/// An event that was stored together with its aggregate and is published later by the <see cref="OutboxProcessor"/>.
/// </summary>
public sealed class OutboxMessage
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
 
    private OutboxMessage()
    {
        // EF Core
    }
 
    /// <summary>Used as message id when publishing, so consumers can detect duplicates.</summary>
    public Guid Id { get; private set; }
 
    /// <summary>CLR type of the event: <c>Namespace.Type, Assembly</c> (without version).</summary>
    public string EventType { get; private set; } = null!;
 
    public string Payload { get; private set; } = null!;
 
    public DateTimeOffset OccurredAt { get; private set; }
 
    public string? TraceParent { get; private set; }
 
    public string? TraceState { get; private set; }
 
    public string? Baggage { get; private set; }
 
    /// <summary>Set once the event was handed to the transport.</summary>
    public DateTimeOffset? ProcessedAt { get; private set; }
 
    public int Attempts { get; private set; }
 
    public string? LastError { get; private set; }
 
    /// <summary>Set if the entry can never be published (unknown type, invalid payload).</summary>
    public DateTimeOffset? FailedAt { get; private set; }
 
    /// <summary>
    /// Call this in the flow that raised the event (e.g. <c>Repository.Store</c>): the current trace context is captured here.
    /// </summary>
    public static OutboxMessage Create(IDomainEvent @event, DateTimeOffset occurredAt) => CreateFor(@event, occurredAt);
 
    /// <inheritdoc cref="Create(IDomainEvent, DateTimeOffset)"/>
    public static OutboxMessage Create(IIntegrationEvent @event, DateTimeOffset occurredAt) => CreateFor(@event, occurredAt);
 
    private static OutboxMessage CreateFor(object @event, DateTimeOffset occurredAt)
    {
        var type = @event.GetType();
        var traceContext = MessageTraceContext.Capture();
        occurredAt = occurredAt.ToUniversalTime(); // Npgsql only accepts UTC for timestamptz
 
        return new OutboxMessage
        {
            Id = Guid.CreateVersion7(occurredAt),
            EventType = $"{type.FullName}, {type.Assembly.GetName().Name}",
            Payload = JsonSerializer.Serialize(@event, type, JsonOptions),
            OccurredAt = occurredAt,
            TraceParent = traceContext?.TraceParent,
            TraceState = traceContext?.TraceState,
            Baggage = traceContext?.Baggage,
        };
    }
 
    internal bool TryRestoreEvent([NotNullWhen(true)] out object? @event, [NotNullWhen(false)] out string? error)
    {
        @event = null;
        var type = Type.GetType(EventType, throwOnError: false);
        if (type is null || !(typeof(IDomainEvent).IsAssignableFrom(type) || typeof(IIntegrationEvent).IsAssignableFrom(type)))
        {
            error = $"'{EventType}' is not a known domain or integration event.";
            return false;
        }
 
        try
        {
            @event = JsonSerializer.Deserialize(Payload, type, JsonOptions);
            error = @event is null ? "The payload is null." : null;
        }
        catch (JsonException exception)
        {
            error = exception.Message;
        }
 
        return @event is not null;
    }
 
    internal PublishOptions ToPublishOptions() => new()
    {
        MessageId = Id,
        Timestamp = OccurredAt,
        TraceContext = TraceParent is null ? null : new MessageTraceContext(TraceParent, TraceState, Baggage),
    };
 
    internal void MarkProcessed(DateTimeOffset now)
    {
        Attempts++;
        ProcessedAt = now;
        LastError = null;
    }
 
    internal void RecordFailure(string error)
    {
        Attempts++;
        LastError = error;
    }
 
    internal void MarkFailed(DateTimeOffset now, string error)
    {
        RecordFailure(error);
        FailedAt = now;
    }
}