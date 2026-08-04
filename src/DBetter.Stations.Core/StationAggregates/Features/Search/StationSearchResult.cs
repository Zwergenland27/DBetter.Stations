using DBetter.Stations.Core.StationAggregates.ValueObjects;

namespace DBetter.Stations.Core.StationAggregates.Features.Search;

public class StationSearchResult
{
    public required StationId Id { get; init; }
    
    public required EvaNumber EvaNumber { get; init; }
    
    public required StationName StationName { get; init; }
    
    public required Ril100Code? Rl100Code { get; init; }
}