namespace DBetter.Stations.Infrastructure.Common;

public sealed record OpeningPeriodPersistenceDto
{
    public required TimeOnly Opens { get; init; }
    public required TimeSpan Duration { get; init; }
}