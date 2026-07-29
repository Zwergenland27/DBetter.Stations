namespace DBetter.Stations.Application.StationFeatures.Search.Ports;

public interface IExternalStationSearchProvider
{
    Task<IReadOnlyList<ExternalStationSearchDto>> SearchAsync(string query);
}