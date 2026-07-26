using DBetter.Stations.Domain.StationAggregates;
using DBetter.Stations.Domain.StationAggregates.ValueObjects;
using DBetter.Stations.Infrastructure.Database;

namespace DBetter.Stations.Infrastructure.StationAggregates;

internal class StationPersistenceDto: IPersistenceDto<Station, StationPersistenceDto>
{
    public required Guid Id { get; init; }
    
    public required int EvaNumber { get; init; }
    
    public required string OfficialName { get; set; }
    
    public required float? Latitude { get; set; }
    
    public required float? Longitude { get; set; }
    
    public required short? StationCategory { get; set; }
    
    public required string? Ril100Code { get; set; }
    
    public required int? InfoId { get; set; }
    
    public required bool? ServesHighSpeedTrains { get; set; }
    public required bool? ServesFastTrains { get; set; }
    public required bool? ServesUnpartneredFastTrains { get; set; }
    public required bool? ServesRegionalTrains { get; set; }
    public required bool? ServesSuburbanTrains { get; set; }
    public required bool? ServesUndergroundTrains { get; set; }
    public required bool? ServesTrams { get; set; }
    public required bool? ServesBusses { get; set; }
    public required bool? ServesBoats { get; set; }
    
    public Station ToDomain()
    {
        Coordinates? location = Latitude is not null && Longitude is not null
            ? new Coordinates(Latitude.Value, Longitude.Value)
            : null;
        
        var serves = ServesHighSpeedTrains is not null
            ? new ServedTransportModes(
                ServesHighSpeedTrains.Value,
                ServesFastTrains!.Value,
                ServesUnpartneredFastTrains!.Value,
                ServesRegionalTrains!.Value,
                ServesSuburbanTrains!.Value,
                ServesUndergroundTrains!.Value,
                ServesTrams!.Value,
                ServesBusses!.Value,
                ServesBoats!.Value)
            : null;
        
        return new Station(
            new StationId(Id),
            new EvaNumber(EvaNumber),
            new StationName(OfficialName),
            location,
            StationCategory is not null ? new StationCategory(StationCategory.Value) : null,
            Ril100Code is not null ? new Ril100Code(Ril100Code) : null,
            InfoId is not null ? new StationInfoId(InfoId.Value) : null,
            serves);
    }

    public void Apply(Station domain)
    {
        OfficialName = domain.OfficialName.Value;
        Latitude = domain.Location?.Latitude;
        Longitude = domain.Location?.Longitude;
        StationCategory = domain.StationCategory?.Value;
        Ril100Code = domain.Ril100Code?.Value;
        InfoId = domain.InfoId?.Value;
        ServesHighSpeedTrains = domain.Serves?.ServesHighSpeedTrains;
        ServesFastTrains = domain.Serves?.ServesFastTrains;
        ServesUnpartneredFastTrains = domain.Serves?.ServesUnpartneredFastTrains;
        ServesRegionalTrains = domain.Serves?.ServesRegionalTrains;
        ServesSuburbanTrains = domain.Serves?.ServesSuburbanTrains;
        ServesUndergroundTrains = domain.Serves?.ServesUndergroundTrains;
        ServesTrams = domain.Serves?.ServesTrams;
        ServesBusses = domain.Serves?.ServesBusses;
        ServesBoats = domain.Serves?.ServesBoats;
    }

    public static StationPersistenceDto FromDomain(Station domain)
    {
        return new StationPersistenceDto
        {
            Id = domain.Id.Value,
            EvaNumber = domain.EvaNumber.Value,
            OfficialName = domain.OfficialName.Value,
            Latitude = domain.Location?.Latitude,
            Longitude = domain.Location?.Longitude,
            StationCategory = domain.StationCategory?.Value,
            Ril100Code = domain.Ril100Code?.Value,
            InfoId = domain.InfoId?.Value,
            ServesHighSpeedTrains = domain.Serves?.ServesHighSpeedTrains,
            ServesFastTrains = domain.Serves?.ServesFastTrains,
            ServesUnpartneredFastTrains = domain.Serves?.ServesUnpartneredFastTrains,
            ServesRegionalTrains = domain.Serves?.ServesRegionalTrains,
            ServesSuburbanTrains = domain.Serves?.ServesSuburbanTrains,
            ServesUndergroundTrains = domain.Serves?.ServesUndergroundTrains,
            ServesTrams = domain.Serves?.ServesTrams,
            ServesBusses = domain.Serves?.ServesBusses,
            ServesBoats = domain.Serves?.ServesBoats,
        };
    }
}