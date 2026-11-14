namespace DB.MarketPlace.StaDa.Models;

public sealed class Station
{
    /// <summary>
    /// Unique identifier representing a specific railway station
    /// </summary>
    /// <remarks>The id is also known as the infoId</remarks>
    /// <example>1071</example>
    public required int InfoId { get; set; }

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
    /// IFOPT number of the station
    /// </summary>
    /// <example>de:11000:900003201</example>
    public required string? Ifopt { get; set; }

    /// <summary>
    /// Facilities available at the station
    /// </summary>
    public required Facilities Facilities { get; set; }

    /// <summary>
    /// Stepless access to the station
    /// </summary>
    public required StepFreeAccess StepFreeAccess { get; set; }

    /// <summary>
    /// Availability of the mobility service
    /// </summary>
    public required MobilityServiceAvailability MobilityService { get; set; }

    /// <summary>
    /// EVA numbers related to the station
    /// </summary>
    public required List<StationPart> StationParts { get; set; }

    /// <summary>
    /// Mailing address of the station
    /// </summary>
    public required Address? MailingAddress { get; set; }

    /// <summary>
    /// Weekly schedule of DB information desk of the station
    /// </summary>
    /// <remarks>Only set when the station has an information desk</remarks>
    public required OpeningHours? DbInformation { get; set; }

    /// <summary>
    /// Weekly schedule of the local service staff
    /// </summary>
    /// <remarks>Only set when the station has local service staff</remarks>
    public required OpeningHours? LocalServiceStaff { get; set; }

    /// <summary>
    /// Mobility service staff of the station
    /// </summary>
    /// <remarks>Only set when the station has mobility service staff</remarks>
    public required OpeningHours? MobilityServiceStaff { get; set; }
}