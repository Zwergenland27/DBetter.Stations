namespace DBetter.Stations.Infrastructure.Messaging;

public sealed class OutboxOptions
{
    public const string SectionName = "Outbox";
    
    /// <summary>Time between two runs. Defaults to 10 seconds.</summary>
    public TimeSpan PollingInterval { get; set; } = TimeSpan.FromSeconds(10);
 
    /// <summary>Entries locked and published per transaction. Defaults to 100.</summary>
    public int BatchSize { get; set; } = 100;
 
    /// <summary>
    /// How long processed entries are kept (for debugging). <c>null</c> keeps them forever. Defaults to 7 days.
    /// </summary>
    public TimeSpan? Retention { get; set; } = TimeSpan.FromDays(7);
 
    /// <summary>How often processed entries older than <see cref="Retention"/> are deleted. Defaults to 1 hour.</summary>
    public TimeSpan CleanupInterval { get; set; } = TimeSpan.FromHours(1);
 
    internal void Validate()
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(PollingInterval, TimeSpan.Zero, nameof(PollingInterval));
        ArgumentOutOfRangeException.ThrowIfLessThan(BatchSize, 1, nameof(BatchSize));
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(CleanupInterval, TimeSpan.Zero, nameof(CleanupInterval));
        if (Retention is { } retention)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(retention, TimeSpan.Zero, nameof(Retention));
        }
    }
}