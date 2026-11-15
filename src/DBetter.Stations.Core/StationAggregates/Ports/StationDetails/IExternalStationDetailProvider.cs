using DBetter.Stations.Core.StationAggregates.ValueObjects;

namespace DBetter.Stations.Core.StationAggregates.Ports.StationDetails;

public interface IExternalStationDetailProvider
{
    Task<StationDetailSnapshot?> GetStationDetailsAsync(EvaNumber evaNumber, CancellationToken cancellationToken);
}