namespace DBetter.Stations.Application.StationFeatures.Search.Ports;

public interface IStationSearchService
{
    public Task<IEnumerable<StationSearchResult>> SearchAsync(string query, CancellationToken cancellationToken);
}