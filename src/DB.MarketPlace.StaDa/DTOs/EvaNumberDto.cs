namespace DB.MarketPlace.StaDa.DTOs;

internal sealed class EvaNumberDto
{
    /// <summary>
    /// EVA identifier
    /// </summary>
    /// <example>8011160</example>
    public required long Number { get; set; }

    /// <summary>
    /// Whether this is the main EVA number of the station
    /// </summary>
    /// <remarks>Supported for compatibility reasons only, has no business background in terms of DB InfraGO AG</remarks>
    public bool? IsMain { get; set; }

    /// <summary>
    /// Position of the EVA number as GeoJSON point
    /// </summary>
    public GeoJsonPointDto? GeographicCoordinates { get; set; }
}