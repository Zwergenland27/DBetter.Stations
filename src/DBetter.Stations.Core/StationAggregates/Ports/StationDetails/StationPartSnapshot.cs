using DBetter.Stations.Core.StationAggregates.ValueObjects;

namespace DBetter.Stations.Core.StationAggregates.Ports.StationDetails;

public record StationPartSnapshot
{
    public required EvaNumber EvaNumber { get; init; }
    
    public required bool IsMain { get; init; }
}