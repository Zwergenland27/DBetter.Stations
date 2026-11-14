namespace DB.MarketPlace.StaDa.DTOs;

internal sealed class DbInformationDto
{
    /// <summary>
    /// Weekly opening schedule of the information desk
    /// </summary>
    public WeeklyScheduleDto? Availability { get; set; }

    /// <summary>
    /// Floor of the information desk
    /// </summary>
    public string? FloorType { get; set; }

    /// <summary>
    /// Description of the location within the station
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Latitude of the information desk (WGS84)
    /// </summary>
    /// <example>52.525592</example>
    public double? Latitude { get; set; }

    /// <summary>
    /// Longitude of the information desk (WGS84)
    /// </summary>
    /// <example>13.369545</example>
    public double? Longitude { get; set; }
}