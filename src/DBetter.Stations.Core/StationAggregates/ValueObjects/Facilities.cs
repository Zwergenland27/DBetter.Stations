namespace DBetter.Stations.Core.StationAggregates.ValueObjects;

public record Facilities
{
    /// <summary>
    /// Public bicycle parking available
    /// </summary>
    public bool HasBicycleParking { get; }

    /// <summary>
    /// Car sharing or car rental available
    /// </summary>
    public bool HasCarRental { get; }

    /// <summary>
    /// DB lounge available
    /// </summary>
    public bool HasDbLounge { get; }

    /// <summary>
    /// Local public transport available
    /// </summary>
    public bool HasLocalPublicTransport { get; }

    /// <summary>
    /// Locker system available
    /// </summary>
    public bool HasLockerSystem { get; }

    /// <summary>
    /// Lost and found available
    /// </summary>
    public bool HasLostAndFound { get; }

    /// <summary>
    /// Public parking available
    /// </summary>
    public bool HasParking { get; }

    /// <summary>
    /// Public facilities available
    /// </summary>
    public bool HasPublicFacilities { get; }

    /// <summary>
    /// Railway mission available
    /// </summary>
    public bool HasRailwayMission { get; }

    /// <summary>
    /// Taxi rank in front of the station available
    /// </summary>
    public bool HasTaxiRank { get; }

    /// <summary>
    /// Local travel center available
    /// </summary>
    public bool HasTravelCenter { get; }

    /// <summary>
    /// Shop for travel necessities available
    /// </summary>
    public bool HasTravelNecessities { get; }

    /// <summary>
    /// Public Wi-Fi available
    /// </summary>
    public bool HasWifi { get; }

    public Facilities(bool hasBicycleParking, bool hasCarRental, bool hasDbLounge, bool hasLocalPublicTransport,
        bool hasLockerSystem, bool hasLostAndFound, bool hasParking, bool hasPublicFacilities, bool hasRailwayMission,
        bool hasTaxiRank, bool hasTravelCenter, bool hasTravelNecessities, bool hasWifi)
    {
        HasBicycleParking = hasBicycleParking;
        HasCarRental = hasCarRental;
        HasDbLounge = hasDbLounge;
        HasLocalPublicTransport = hasLocalPublicTransport;
        HasLockerSystem = hasLockerSystem;
        HasLostAndFound = hasLostAndFound;
        HasParking = hasParking;
        HasPublicFacilities = hasPublicFacilities;
        HasRailwayMission = hasRailwayMission;
        HasTaxiRank = hasTaxiRank;
        HasTravelCenter = hasTravelCenter;
        HasTravelNecessities = hasTravelNecessities;
        HasWifi = hasWifi;
    }
}