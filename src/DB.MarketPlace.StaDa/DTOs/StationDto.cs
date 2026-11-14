namespace DB.MarketPlace.StaDa.DTOs;

internal sealed class StationDto
{
    /// <summary>
    /// Unique identifier representing a specific railway station
    /// </summary>
    /// <example>1071</example>
    public required int Number { get; set; }

    /// <summary>
    /// Name of the station
    /// </summary>
    /// <example>Berlin Hbf</example>
    public required string Name { get; set; }

    /// <summary>
    /// Category of the station (-1...7)
    /// </summary>
    /// <remarks>Stations with category -1 or 0 are not in production, e.g. planned, sold, without train stops</remarks>
    /// <example>1</example>
    public required int Category { get; set; }

    /// <summary>
    /// Determines in some respect the price for train stops at the station (1...7)
    /// </summary>
    /// <example>1</example>
    public int? PriceCategory { get; set; }

    /// <summary>
    /// ISO code of the country
    /// </summary>
    /// <example>de</example>
    public string? CountryCode { get; set; }

    /// <summary>
    /// German federal state
    /// </summary>
    /// <example>Berlin</example>
    public string? FederalState { get; set; }

    /// <summary>
    /// ISO code of the federal state
    /// </summary>
    /// <example>DE-BE</example>
    public string? FederalStateCode { get; set; }

    /// <summary>
    /// Numeric code used for the identification of communities
    /// </summary>
    /// <example>11000000</example>
    public string? MunicipalityCode { get; set; }

    /// <summary>
    /// IFOPT number of the station
    /// </summary>
    /// <example>de:11000:900003201</example>
    public string? Ifopt { get; set; }

    /// <summary>
    /// Public bicycle parking available
    /// </summary>
    public bool? HasBicycleParking { get; set; }

    /// <summary>
    /// Car sharing or car rental available
    /// </summary>
    public bool? HasCarRental { get; set; }

    /// <summary>
    /// DB lounge available
    /// </summary>
    public bool? HasDBLounge { get; set; }

    /// <summary>
    /// Local public transport available
    /// </summary>
    public bool? HasLocalPublicTransport { get; set; }

    /// <summary>
    /// Locker system available
    /// </summary>
    public bool? HasLockerSystem { get; set; }

    /// <summary>
    /// Lost and found available
    /// </summary>
    public bool? HasLostAndFound { get; set; }

    /// <summary>
    /// Availability of the mobility service as free text
    /// </summary>
    /// <remarks>
    /// Known values are 'no', 'Nur nach Voranmeldung unter 030 65 21 28 88 (Ortstarif)'
    /// and 'Ja, um Voranmeldung unter 030 65 21 28 88 (Ortstarif) wird gebeten'
    /// </remarks>
    /// <example>no</example>
    public string? HasMobilityService { get; set; }

    /// <summary>
    /// Public parking available
    /// </summary>
    public bool? HasParking { get; set; }

    /// <summary>
    /// Public facilities available
    /// </summary>
    public bool? HasPublicFacilities { get; set; }

    /// <summary>
    /// Railway mission available
    /// </summary>
    public bool? HasRailwayMission { get; set; }

    /// <summary>
    /// Stepless access to the station
    /// </summary>
    /// <remarks>Known values are 'yes', 'no' and 'partial'</remarks>
    /// <example>yes</example>
    public string? HasSteplessAccess { get; set; }

    /// <summary>
    /// Taxi rank in front of the station available
    /// </summary>
    public bool? HasTaxiRank { get; set; }

    /// <summary>
    /// Local travel center available
    /// </summary>
    public bool? HasTravelCenter { get; set; }

    /// <summary>
    /// Shop for travel necessities available
    /// </summary>
    public bool? HasTravelNecessities { get; set; }

    /// <summary>
    /// Public Wi-Fi available
    /// </summary>
    public bool? HasWiFi { get; set; }

    /// <summary>
    /// DB information desk of the station
    /// </summary>
    public DbInformationDto? DBInformation { get; set; }

    /// <summary>
    /// Local public sector entity responsible for short distance public transport in the area
    /// </summary>
    public AufgabentraegerDto? Aufgabentraeger { get; set; }

    /// <summary>
    /// EVA numbers related to the station
    /// </summary>
    public List<EvaNumberDto>? EvaNumbers { get; set; }

    /// <summary>
    /// Ril100 identifiers related to the station
    /// </summary>
    public List<Ril100IdentifierDto>? Ril100Identifiers { get; set; }

    /// <summary>
    /// Weekly schedule of the local service staff
    /// </summary>
    public LocalServiceStaffDto? LocalServiceStaff { get; set; }

    /// <summary>
    /// Mobility service staff of the station
    /// </summary>
    public MobilityServiceStaffDto? MobilityServiceStaff { get; set; }

    /// <summary>
    /// Official secondary names of the station in local minority languages
    /// </summary>
    public LocalizedNamesDto? LocalizedNames { get; set; }

    /// <summary>
    /// Mailing address of the station
    /// </summary>
    public AddressDto? MailingAddress { get; set; }

    /// <summary>
    /// Regional department of DB InfraGO AG responsible for the station
    /// </summary>
    public RegionalbereichDto? Regionalbereich { get; set; }

    /// <summary>
    /// Station management responsible for the station
    /// </summary>
    public StationManagementDto? StationManagement { get; set; }

    /// <summary>
    /// 3-S-Zentrale (24/7 operating center) responsible for the station
    /// </summary>
    public SZentraleDto? Szentrale { get; set; }

    /// <summary>
    /// Timetable office responsible for the station
    /// </summary>
    public TimeTableOfficeDto? TimeTableOffice { get; set; }

    /// <summary>
    /// Wireless LAN installation of the station
    /// </summary>
    public WirelessLanDto? WirelessLan { get; set; }
}