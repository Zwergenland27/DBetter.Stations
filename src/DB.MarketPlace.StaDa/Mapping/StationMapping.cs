using DB.MarketPlace.StaDa.DTOs;
using DB.MarketPlace.StaDa.Models;

namespace DB.MarketPlace.StaDa.Mapping;

internal class StationMapping
{
    internal static Station Map(StationDto dto)
    {
        return new Station
        {
            InfoId = dto.Number,
            Name = dto.Name,
            Category = dto.Category,
            Ifopt = dto.Ifopt,
            Facilities = MapFacilities(dto),
            StepFreeAccess = StepFreeAccessMapping.Map(dto.HasSteplessAccess),
            MobilityService = MobilityServiceMapping.Map(dto.HasMobilityService),
            StationParts = (dto.EvaNumbers ?? []).Select(MapStationPart).ToList(),
            MailingAddress = MapAddress(dto.MailingAddress),
            DbInformation = dto.DBInformation?.Availability.Map(),
            LocalServiceStaff = dto.LocalServiceStaff?.Availability.Map(),
            MobilityServiceStaff = dto.MobilityServiceStaff?.Availability.Map()
        };
    }
    
    private static Facilities MapFacilities(StationDto dto)
    {
        return new Facilities
        {
            HasBicycleParking = dto.HasBicycleParking ?? false,
            HasCarRental = dto.HasCarRental ?? false,
            HasDbLounge = dto.HasDBLounge ?? false,
            HasLocalPublicTransport = dto.HasLocalPublicTransport ?? false,
            HasLockerSystem = dto.HasLockerSystem ?? false,
            HasLostAndFound = dto.HasLostAndFound ?? false,
            HasParking = dto.HasParking ?? false,
            HasPublicFacilities = dto.HasPublicFacilities ?? false,
            HasRailwayMission = dto.HasRailwayMission ?? false,
            HasTaxiRank = dto.HasTaxiRank ?? false,
            HasTravelCenter = dto.HasTravelCenter ?? false,
            HasTravelNecessities = dto.HasTravelNecessities ?? false,
            HasWiFi = dto.HasWiFi ?? false
        };
    }
    
    private static StationPart MapStationPart(EvaNumberDto dto)
    {
        return new StationPart
        {
            Number = dto.Number.ToString(),
            IsMain = dto.IsMain ?? false,
        };
    }
    
    private static Address? MapAddress(AddressDto? dto)
    {
        if (dto is null)
        {
            return null;
        }

        return new Address
        {
            Street = dto.Street,
            HouseNumber = dto.HouseNumber,
            Zipcode = dto.Zipcode,
            City = dto.City
        };
    }
}