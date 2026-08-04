using DBetter.Stations.Core.StationAggregates.ValueObjects;

namespace DBetter.Stations.Core.StationAggregates.Features.Search.Ports;

public interface IInternalStationSearchService
{
    Task<IReadOnlyList<StationSearchDto>> SearchAsync(string name);

    Task<StationSearchDto?> GetAsync(Ril100Code ril100);

    Task<StationSearchDto?> GetAsync(EvaNumber evaNumber);

    Task<IReadOnlyList<StationSearchDto>> ResolveManyAsync(IEnumerable<EvaNumber> evaNumbers);
}