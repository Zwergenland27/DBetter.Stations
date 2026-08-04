namespace DBetter.Stations.Core.StationAggregates.Features.Search;

public class StationSearchResult
{
    public required string Id { get; init; }
    
    public required int EvaNumber { get; init; }
    
    public required string StationName { get; init; }
    
    public required string? Rl100Code { get; init; }
}