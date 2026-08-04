namespace DBetter.Stations.Core.StationAggregates.Features.Search.Ports;

public interface IExternalStationSearchProvider
{
    Task<IReadOnlyList<ExternalStationSearchDto>> SearchAsync(string query);
}