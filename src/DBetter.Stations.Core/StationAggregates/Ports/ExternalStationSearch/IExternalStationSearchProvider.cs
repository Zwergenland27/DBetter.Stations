using DBetter.Stations.Core.StationAggregates.ValueObjects;

namespace DBetter.Stations.Core.StationAggregates.Ports.ExternalStationSearch;

public interface IExternalStationSearchProvider
{
    Task<IReadOnlyList<ExternalStationSearchDto>> SearchAsync(string query);

    Task<ExternalStationSearchDto?> FindMatchAsync(StationName officialName, EvaNumber evaNumber);
}