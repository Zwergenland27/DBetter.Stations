using DBetter.Stations.Core.StationAggregates.ValueObjects;

namespace DBetter.Stations.Core.StationAggregates.Ports.StationDetails;

public record StationDetailSnapshot
{
    public required List<StationPartSnapshot> StationParts { get; init; }
    
    public required StationCategory? Category { get; init; }
    
    public required IfOpt? IfOpt { get; init; }
    
    public required StationInfoId? InfoId { get; init; }
    
    public required ValueObjects.StationDetails Details { get; init; }
}