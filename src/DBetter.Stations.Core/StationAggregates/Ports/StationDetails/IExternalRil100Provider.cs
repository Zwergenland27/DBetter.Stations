using DBetter.Stations.Core.StationAggregates.ValueObjects;

namespace DBetter.Stations.Core.StationAggregates.Ports.StationDetails;

public interface IExternalRil100Provider
{
    Task<Ril100Code?> GetRil100Async(EvaNumber evaNumber, CancellationToken cancellationToken);
}