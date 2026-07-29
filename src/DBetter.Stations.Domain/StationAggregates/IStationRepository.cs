using DBetter.Stations.Domain.StationAggregates.ValueObjects;

namespace DBetter.Stations.Domain.StationAggregates;

public interface IStationRepository
{
    Task<Station?> GetAsync(StationId stationId);
    
    void Save(Station station);

    void SaveMany(IEnumerable<Station> stations);
}