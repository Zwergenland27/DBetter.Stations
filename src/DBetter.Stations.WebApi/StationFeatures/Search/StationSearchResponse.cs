namespace DBetter.Stations.WebApi.StationFeatures.Search;

/// <summary>
/// Represents a single station search result returned by the station search API endpoint.
/// </summary>
public sealed class StationSearchResponse
{
    /// <summary>
    /// The unique identifier of the station in the dbetter system
    /// </summary>
    /// <example>19a9eb33-2441-4fbf-aecf-f90f1aa6d902</example>
    public required string Id { get; init; }

    /// <summary>
    /// The UIC/EVA number uniquely identifying the station within the Deutsche Bahn system.
    /// </summary>
    /// <example>8000105</example>
    public required int EvaNumber { get; init; }

    /// <summary>
    /// The official display name of the station.
    /// </summary>
    /// <example>Frankfurt(Main)Hbf</example>
    public required string StationName { get; init; }

    /// <summary>
    /// The RIL100/DS100 abbreviation code of the station, if available.
    /// Not all stations have an assigned RIL100 code (e.g. some smaller or foreign stops).
    /// </summary>
    /// <example>FF</example>
    public required string? Rl100Code { get; init; }
}