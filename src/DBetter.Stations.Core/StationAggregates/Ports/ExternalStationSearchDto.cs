using DBetter.Stations.Core.StationAggregates.ValueObjects;

namespace DBetter.Stations.Core.StationAggregates.Ports;

public class ExternalStationSearchDto
{
    public required EvaNumber EvaNumber { get; init; }
    
    public required StationName OfficialName { get; init; }
    
    public required Coordinates Location { get; init; }
    
    public required ServedTransportModes Serves { get; init; }
}