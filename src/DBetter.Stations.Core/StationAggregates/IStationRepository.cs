using DBetter.Stations.Core.StationAggregates.ValueObjects;

namespace DBetter.Stations.Core.StationAggregates;

public interface IStationRepository
{
    Task<Station?> GetAsync(StationId stationId);
    
    void Save(Station station);

    void SaveMany(IEnumerable<Station> stations);
}