using DBetter.Stations.Core.Abstractions;
using DBetter.Stations.Core.StationAggregates.Events;
using DBetter.Stations.Core.StationAggregates.ValueObjects;

namespace DBetter.Stations.Core.StationAggregates;

public class Station: AggregateRoot<StationId>
{
    public EvaNumber EvaNumber { get; private init; }
    
    public StationName OfficialName { get; private set; }
    
    public Coordinates? Location { get; private set; }
    
    public StationCategory? StationCategory { get; private set; }
    
    public Ril100Code? Ril100Code { get; private set; }
    
    public StationInfoId? InfoId { get; private set; }
    
    public ServedTransportModes? Serves { get; private set; }
    
    internal Station(StationId id,
        EvaNumber evaNumber,
        StationName officialName,
        Coordinates? location,
        StationCategory? stationCategory,
        Ril100Code? ril100Code,
        StationInfoId? infoId,
        ServedTransportModes? serves) : base(id)
    {
        EvaNumber = evaNumber;
        OfficialName = officialName;
        Location = location;
        StationCategory = stationCategory;
        Ril100Code = ril100Code;
        InfoId = infoId;
        Serves = serves;
    }

    public static Station Create(
        EvaNumber evaNumber,
        StationName officialName,
        Coordinates? location,
        StationCategory? stationCategory,
        Ril100Code? ril100Code,
        StationInfoId? infoId,
        ServedTransportModes? serves)
    {
        var station = new Station(
            StationId.CreateNew(),
            evaNumber,
            officialName,
            location,
            stationCategory,
            ril100Code,
            infoId,
            serves);

        station.RaiseDomainEvent(new NewStationCreatedDomainEvent(station.Id));

        return station;
    }
}