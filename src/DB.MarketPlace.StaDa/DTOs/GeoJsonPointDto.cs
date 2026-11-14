namespace DB.MarketPlace.StaDa.DTOs;

internal sealed class GeoJsonPointDto
{
    /// <summary>
    /// Type of the GeoJSON object
    /// </summary>
    /// <remarks>Currently only points without altitude are provided</remarks>
    /// <example>Point</example>
    public string? Type { get; set; }

    /// <summary>
    /// Coordinates of the point (WGS84)
    /// </summary>
    /// <remarks>First value is longitude, second latitude, third altitude (currently not provided)</remarks>
    /// <example>[13.369549, 52.525589]</example>
    public List<double>? Coordinates { get; set; }
}