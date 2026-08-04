namespace DBetter.Stations.Application.StationFeatures.Search;

public interface IStationSearchService
{
    public Task<IEnumerable<StationSearchResult>> SearchAsync(string query, CancellationToken cancellationToken);
}