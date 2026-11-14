namespace DB.MarketPlace.StaDa.Models;

public sealed class Facilities
{
    /// <summary>
    /// Public bicycle parking available
    /// </summary>
    public required bool HasBicycleParking { get; set; }

    /// <summary>
    /// Car sharing or car rental available
    /// </summary>
    public required bool HasCarRental { get; set; }

    /// <summary>
    /// DB lounge available
    /// </summary>
    public required bool HasDbLounge { get; set; }

    /// <summary>
    /// Local public transport available
    /// </summary>
    public required bool HasLocalPublicTransport { get; set; }

    /// <summary>
    /// Locker system available
    /// </summary>
    public required bool HasLockerSystem { get; set; }

    /// <summary>
    /// Lost and found available
    /// </summary>
    public required bool HasLostAndFound { get; set; }

    /// <summary>
    /// Public parking available
    /// </summary>
    public required bool HasParking { get; set; }

    /// <summary>
    /// Public facilities available
    /// </summary>
    public required bool HasPublicFacilities { get; set; }

    /// <summary>
    /// Railway mission available
    /// </summary>
    public required bool HasRailwayMission { get; set; }

    /// <summary>
    /// Taxi rank in front of the station available
    /// </summary>
    public required bool HasTaxiRank { get; set; }

    /// <summary>
    /// Local travel center available
    /// </summary>
    public required bool HasTravelCenter { get; set; }

    /// <summary>
    /// Shop for travel necessities available
    /// </summary>
    public required bool HasTravelNecessities { get; set; }

    /// <summary>
    /// Public Wi-Fi available
    /// </summary>
    public required bool HasWiFi { get; set; }
}