using System.ComponentModel.DataAnnotations;
using DBetter.Stations.Core.Common;
using DBetter.Stations.Core.StationAggregates;
using DBetter.Stations.Core.StationAggregates.ValueObjects;
using DBetter.Stations.Infrastructure.Common;
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
    
    public required string? IfOpt { get; set; }
    
    public required bool? ServesHighSpeedTrains { get; set; }
    public required bool? ServesFastTrains { get; set; }
    public required bool? ServesUnpartneredFastTrains { get; set; }
    public required bool? ServesRegionalTrains { get; set; }
    public required bool? ServesSuburbanTrains { get; set; }
    public required bool? ServesUndergroundTrains { get; set; }
    public required bool? ServesTrams { get; set; }
    public required bool? ServesBusses { get; set; }
    public required bool? ServesBoats { get; set; }
    
    public required string? StepFreeAccess { get; set; }
    
    public required string? MobilityServiceAvailability { get; set; }
    
    [MaxLength(Address.StreetMaxLength)]
    public required string? AddressStreet { get; set; }
    
    [MaxLength(Address.HouseNumberMaxLength)]
    public required string? AddressHouseNumber { get; set; }
    
    [MaxLength(Address.ZipCodeLength)]
    public required string? AddressZipCode { get; set; }
    
    [MaxLength(Address.CityMaxLength)]
    public required string? AddressCity { get; set; }
    
    public required bool? HasParking { get; set; }
    public required bool? HasBicycleParking { get; set; }
    public required bool? HasLocalPublicTransport { get; set; }
    public required bool? HasPublicFacilities { get; set; }
    public required bool? HasLockerSystem { get; set; }
    public required bool? HasTaxiRank { get; set; }
    public required bool? HasTravelNecessities { get; set; }
    public required bool? HasWifi { get; set; }
    public required bool? HasTravelCenter { get; set; }
    public required bool? HasRailwayMission { get; set; }
    public required bool? HasDbLounge { get; set; }
    public required bool? HasLostAndFound { get; set; }
    public required bool? HasCarRental { get; set; }
    
    public required string? DbInformationOpeningHours { get; set; }
    
    public required string? LocalServiceStaffOpeningHours { get; set; }
    
    public required string? MobilityServiceStaffOpeningHours { get; set; }
    
    public Station ToDomain()
    {
        Coordinates? location = Latitude is not null && Longitude is not null
            ? new Coordinates(Latitude.Value, Longitude.Value)
            : null;

        var serves = ExtractServedTransportModes();

        var details = ExtractDetails();
        
        return new Station(
            new StationId(Id),
            new EvaNumber(EvaNumber),
            new StationName(OfficialName),
            location,
            StationCategory is not null ? new StationCategory(StationCategory.Value) : null,
            Ril100Code is not null ? new Ril100Code(Ril100Code) : null,
            InfoId is not null ? new StationInfoId(InfoId.Value) : null,
            serves,
            IfOpt is not null ? new IfOpt(IfOpt) : null,
            details);
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
        IfOpt = domain.IfOpt?.Value;
        StepFreeAccess = domain.StepFreeAccess?.ToString();
        MobilityServiceAvailability = domain.MobilityServiceAvailability?.ToString();
        AddressStreet = domain.Address?.Street;
        AddressHouseNumber = domain.Address?.HouseNumber;
        AddressZipCode = domain.Address?.ZipCode;
        AddressCity = domain.Address?.City;
        HasParking = domain.Facilities?.HasParking;
        HasBicycleParking = domain.Facilities?.HasBicycleParking;
        HasLocalPublicTransport = domain.Facilities?.HasLocalPublicTransport;
        HasPublicFacilities = domain.Facilities?.HasPublicFacilities;
        HasLockerSystem = domain.Facilities?.HasLockerSystem;
        HasTaxiRank = domain.Facilities?.HasTaxiRank;
        HasTravelNecessities = domain.Facilities?.HasTravelNecessities;
        HasWifi = domain.Facilities?.HasWifi;
        HasTravelCenter = domain.Facilities?.HasTravelCenter;
        HasRailwayMission = domain.Facilities?.HasRailwayMission;
        HasDbLounge = domain.Facilities?.HasDbLounge;
        HasLostAndFound = domain.Facilities?.HasLostAndFound;
        HasCarRental = domain.Facilities?.HasCarRental;
        DbInformationOpeningHours = OpeningHoursPersistenceDto.Serialize(domain.DbInformation);
        LocalServiceStaffOpeningHours = OpeningHoursPersistenceDto.Serialize(domain.LocalServiceStaff);
        MobilityServiceStaffOpeningHours = OpeningHoursPersistenceDto.Serialize(domain.MobilityServiceStaff);
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
            IfOpt = domain.IfOpt?.Value,
            StepFreeAccess = domain.StepFreeAccess?.ToString(),
            MobilityServiceAvailability = domain.MobilityServiceAvailability?.ToString(),
            AddressStreet = domain.Address?.Street,
            AddressHouseNumber = domain.Address?.HouseNumber,
            AddressZipCode = domain.Address?.ZipCode,
            AddressCity = domain.Address?.City,
            HasParking = domain.Facilities?.HasParking,
            HasBicycleParking = domain.Facilities?.HasBicycleParking,
            HasLocalPublicTransport = domain.Facilities?.HasLocalPublicTransport,
            HasPublicFacilities = domain.Facilities?.HasPublicFacilities,
            HasLockerSystem = domain.Facilities?.HasLockerSystem,
            HasTaxiRank = domain.Facilities?.HasTaxiRank,
            HasTravelNecessities = domain.Facilities?.HasTravelNecessities,
            HasWifi = domain.Facilities?.HasWifi,
            HasTravelCenter = domain.Facilities?.HasTravelCenter,
            HasRailwayMission = domain.Facilities?.HasRailwayMission,
            HasDbLounge = domain.Facilities?.HasDbLounge,
            HasLostAndFound = domain.Facilities?.HasLostAndFound,
            HasCarRental = domain.Facilities?.HasCarRental,
            DbInformationOpeningHours = OpeningHoursPersistenceDto.Serialize(domain.DbInformation),
            LocalServiceStaffOpeningHours = OpeningHoursPersistenceDto.Serialize(domain.LocalServiceStaff),
            MobilityServiceStaffOpeningHours = OpeningHoursPersistenceDto.Serialize(domain.MobilityServiceStaff),
        };
    }

    public ServedTransportModes? ExtractServedTransportModes()
    {
        if (ServesHighSpeedTrains is null) return null;

        return new ServedTransportModes(
            ServesHighSpeedTrains.Value,
            ServesFastTrains!.Value,
            ServesUnpartneredFastTrains!.Value,
            ServesRegionalTrains!.Value,
            ServesSuburbanTrains!.Value,
            ServesUndergroundTrains!.Value,
            ServesTrams!.Value,
            ServesBusses!.Value,
            ServesBoats!.Value);
    }

    public StationDetails ExtractDetails()
    {
        return new StationDetails
        {
            StepFreeAccess = StepFreeAccess is not null ? Enum.Parse<StepFreeAccess>(StepFreeAccess) : null,
            MobilityServiceAvailability = MobilityServiceAvailability is not null
                ? Enum.Parse<MobilityServiceAvailability>(MobilityServiceAvailability)
                : null,
            Facilities = ExtractFacilities(),
            DbInformation = OpeningHoursPersistenceDto.Deserialize(DbInformationOpeningHours),
            LocalServiceStaff = OpeningHoursPersistenceDto.Deserialize(LocalServiceStaffOpeningHours),
            MobilityServiceStaff = OpeningHoursPersistenceDto.Deserialize(MobilityServiceStaffOpeningHours),
            Address = ExtractAddress()
        };
    }

    public Facilities? ExtractFacilities()
    {
        if (HasParking is null) return null;
 
        return new Facilities(
            HasParking.Value,
            HasBicycleParking!.Value,
            HasLocalPublicTransport!.Value,
            HasPublicFacilities!.Value,
            HasLockerSystem!.Value,
            HasTaxiRank!.Value,
            HasTravelNecessities!.Value,
            HasWifi!.Value,
            HasTravelCenter!.Value,
            HasRailwayMission!.Value,
            HasDbLounge!.Value,
            HasLostAndFound!.Value,
            HasCarRental!.Value);
    }
    
    public Address? ExtractAddress()
    {
        if (AddressStreet is null) return null;
        
        return new Address(
            AddressStreet,
            AddressHouseNumber,
            AddressZipCode!,
            AddressCity!);
    }
}