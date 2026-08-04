namespace DBetter.Stations.Core.StationAggregates.ValueObjects;

/// <summary>
/// The set of transport modes served by a station, modeled as explicit flags.
/// Adding a new mode is a constructor signature change - every call site that
/// creates this record must be updated, so a new mode can't silently be left
/// out of the search-priority ranking below.
/// </summary>
public sealed record ServedTransportModes(
    bool ServesHighSpeedTrains,
    bool ServesFastTrains,
    bool ServesUnpartneredFastTrains,
    bool ServesRegionalTrains,
    bool ServesSuburbanTrains,
    bool ServesUndergroundTrains,
    bool ServesTrams,
    bool ServesBusses,
    bool ServesBoats)
{
    public bool ServesAnyTransportMode =>
        ServesHighSpeedTrains || ServesFastTrains || ServesUnpartneredFastTrains || ServesRegionalTrains ||
        ServesSuburbanTrains || ServesUndergroundTrains || ServesTrams || ServesBusses || ServesBoats;
}
