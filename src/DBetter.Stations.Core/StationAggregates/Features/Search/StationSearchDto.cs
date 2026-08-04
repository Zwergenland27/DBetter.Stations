using DBetter.Stations.Core.StationAggregates.ValueObjects;

namespace DBetter.Stations.Core.StationAggregates.Features.Search;

public class StationSearchDto
{
    public required StationId Id { get; init; }
    
    public required EvaNumber EvaNumber { get; init; }
    
    public required StationName OfficialName { get; init; }
    
    public required Ril100Code? Ril100Code { get; init; }
    
    public required StationCategory? Category { get; init; }
    
    public required ServedTransportModes? Serves { get; init; }
}