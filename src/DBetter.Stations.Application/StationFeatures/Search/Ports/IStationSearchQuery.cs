using DBetter.Stations.Domain.StationAggregates.SearchPriorisation;
using DBetter.Stations.Domain.StationAggregates.ValueObjects;

namespace DBetter.Stations.Application.StationFeatures.Search.Ports;

public interface IStationSearchQuery
{
    Task<IReadOnlyList<StationSearchDto>> SearchAsync(string name);

    Task<StationSearchDto?> GetAsync(Ril100Code ril100);

    Task<StationSearchDto?> GetAsync(EvaNumber evaNumber);

    Task<IReadOnlyList<StationSearchDto>> ResolveManyAsync(IEnumerable<EvaNumber> evaNumbers);
}