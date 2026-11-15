using DB.MarketPlace.StaDa;
using DB.MarketPlace.StaDa.Models;
using DBetter.Stations.Core.Common;
using DBetter.Stations.Core.StationAggregates;
using DBetter.Stations.Core.StationAggregates.Ports.StationDetails;
using DBetter.Stations.Core.StationAggregates.ValueObjects;
using Address = DB.MarketPlace.StaDa.Models.Address;
using Facilities = DB.MarketPlace.StaDa.Models.Facilities;
using StationFacilities = DBetter.Stations.Core.StationAggregates.ValueObjects.Facilities;
using MobilityServiceAvailability = DB.MarketPlace.StaDa.Models.MobilityServiceAvailability;
using OpeningHours = DB.MarketPlace.StaDa.Models.OpeningHours;
using OpeningPeriod = DB.MarketPlace.StaDa.Models.OpeningPeriod;
using Station = DB.MarketPlace.StaDa.Models.Station;
using StepFreeAccess = DB.MarketPlace.StaDa.Models.StepFreeAccess;

namespace DBetter.Stations.Infrastructure.StationAggregates;

public class StadaStationDetailProvider(StaDaService service): IExternalStationDetailProvider
{
    public async Task<StationDetailSnapshot?> GetStationDetailsAsync(EvaNumber evaNumber,
        CancellationToken cancellationToken)
    {
        var stadaInformation = await service.GetStationInformationAsync(evaNumber.Value.ToString(), cancellationToken);
        if (stadaInformation is null) return null;

        StationCategory? category = null;
        var categoryCreationResult = StationCategory.Create(stadaInformation.Category);
        if (!categoryCreationResult.HasFailed)
        {
            category = categoryCreationResult.Value;
        }

        IfOpt? ifOpt = null;
        if (stadaInformation.Ifopt is not null)
        {
            var ifOptCreationResult = IfOpt.Create(stadaInformation.Ifopt);
            if (!ifOptCreationResult.HasFailed)
            {
                ifOpt = ifOptCreationResult.Value;
            }
        }

        StationInfoId? infoId = null;
        var infoIdCreationResult = StationInfoId.Create(stadaInformation.InfoId.ToString());
        if (!infoIdCreationResult.HasFailed)
        {
            infoId = infoIdCreationResult.Value;
        }

        return new StationDetailSnapshot
        {
            StationParts = stadaInformation.StationParts.Select(Map).OfType<StationPartSnapshot>().ToList(),
            Category = category,
            IfOpt = ifOpt,
            InfoId = infoId,
            Details = MapDetails(stadaInformation)
        };
    }

    private static StationPartSnapshot? Map(StationPart stationPart)
    {
        var evaNumberResult = EvaNumber.Create(stationPart.Number);
        if (evaNumberResult.HasFailed) return null;
        return new StationPartSnapshot
        {
            EvaNumber = evaNumberResult.Value,
            IsMain = stationPart.IsMain ?? false
        };
    }

    private static StationDetails MapDetails(Station station)
    {
        return new StationDetails
        {
            Address = MapAddress(station.MailingAddress),
            StepFreeAccess = MapStepFreeAccess(station.StepFreeAccess),
            MobilityServiceAvailability = MapMobilityServiceAvailability(station.MobilityService),
            Facilities = MapFacilities(station.Facilities),
            DbInformation = MapOpeningHours(station.DbInformation),
            LocalServiceStaff = MapOpeningHours(station.LocalServiceStaff),
            MobilityServiceStaff = MapOpeningHours(station.MobilityServiceStaff, true),
        };
    }

    private static Core.Common.Address? MapAddress(Address? mailAddress)
    {
        if (mailAddress is null) return null;
        if (mailAddress.City is null || mailAddress.Zipcode is null || mailAddress.Street is null) return null;

        if (mailAddress.HouseNumber is not null)
        {
            var fullAddressCreationResult = Core.Common.Address.Create(mailAddress.Street, mailAddress.HouseNumber, mailAddress.Zipcode,
                mailAddress.City);
            if (fullAddressCreationResult.HasFailed) return null;
            return fullAddressCreationResult.Value;
        }
        
        var addressCreationResult = Core.Common.Address.Create(mailAddress.Street, mailAddress.Zipcode,
            mailAddress.City);
        if (addressCreationResult.HasFailed) return null;
        return addressCreationResult.Value;
    }

    private static Core.StationAggregates.ValueObjects.StepFreeAccess? MapStepFreeAccess(StepFreeAccess stepFreeAccess)
    {
        return stepFreeAccess switch
        {
            StepFreeAccess.Yes => Core.StationAggregates.ValueObjects.StepFreeAccess.Yes,
            StepFreeAccess.No => Core.StationAggregates.ValueObjects.StepFreeAccess.No,
            StepFreeAccess.Partial => Core.StationAggregates.ValueObjects.StepFreeAccess.Partial,
            _ => null
        };
    }

    private static Core.StationAggregates.ValueObjects.MobilityServiceAvailability? MapMobilityServiceAvailability(
        MobilityServiceAvailability mobilityServiceAvailability)
    {
        return mobilityServiceAvailability switch
        {
            MobilityServiceAvailability.NotAvailable => Core.StationAggregates.ValueObjects.MobilityServiceAvailability
                .NotAvailable,
            MobilityServiceAvailability.OnlyWithPriorRegistration => Core.StationAggregates.ValueObjects
                .MobilityServiceAvailability.OnlyWithPriorRegistration,
            MobilityServiceAvailability.AvailablePriorRegistrationRequested => Core.StationAggregates.ValueObjects
                .MobilityServiceAvailability.AvailablePriorRegistrationRequested,
            _ => null
        };
    }

    private static StationFacilities MapFacilities(Facilities facilities)
    {
        return new StationFacilities(
            facilities.HasBicycleParking,
            facilities.HasCarRental,
            facilities.HasDbLounge,
            facilities.HasLocalPublicTransport,
            facilities.HasLockerSystem,
            facilities.HasLostAndFound,
            facilities.HasParking,
            facilities.HasPublicFacilities,
            facilities.HasRailwayMission,
            facilities.HasTaxiRank,
            facilities.HasTravelCenter,
            facilities.HasTravelNecessities,
            facilities.HasWiFi);
    }

    private static Core.Common.OpeningHours? MapOpeningHours(OpeningHours? openingHours, bool useSundayAsHolidayReference = false)
    {
        if (openingHours is null) return null;

        var all = new[]
        {
            openingHours.Monday, openingHours.Tuesday, openingHours.Wednesday, openingHours.Thursday,
            openingHours.Friday, openingHours.Saturday, openingHours.Sunday, openingHours.Holiday
        };

        if (all.All(openingPeriod => openingPeriod.Count == 0)) return null;

        var holidaySchedule = MapDaySchedule(openingHours.Holiday);
        if (useSundayAsHolidayReference)
        {
            holidaySchedule = MapDaySchedule(openingHours.Sunday);
        }

        return Core.Common.OpeningHours.Create(
            MapDaySchedule(openingHours.Monday),
            MapDaySchedule(openingHours.Tuesday),
            MapDaySchedule(openingHours.Wednesday),
            MapDaySchedule(openingHours.Thursday),
            MapDaySchedule(openingHours.Friday),
            MapDaySchedule(openingHours.Saturday),
            MapDaySchedule(openingHours.Sunday),
            holidaySchedule);
    }

    private static DaySchedule MapDaySchedule(List<OpeningPeriod> openingPeriods)
    {
        if (openingPeriods.Count == 0) return DaySchedule.Closed();

        return DaySchedule.Create(openingPeriods.Select(MapOpeningPeriod).OfType<Core.Common.OpeningPeriod>());
    }

    private static Core.Common.OpeningPeriod? MapOpeningPeriod(OpeningPeriod openingPeriod)
    {
        var openingPeriodCreationResult = Core.Common.OpeningPeriod.Create(openingPeriod.FromTime, openingPeriod.ToTime);
        if (openingPeriodCreationResult.HasFailed) return null;
        return openingPeriodCreationResult.Value;
    }
}