namespace BahnDe.Stations;

internal class Ort
{
    /// <summary>
    /// Eva number, if place is a station
    /// </summary>
    /// <remarks>Only set when place is a station</remarks>
    /// <example>8010085</example>
    public string? ExtId { get; set; }
    
    /// <summary>
    /// Name of the place
    /// </summary>
    /// <example>Dresden Hbf</example>
    public required string Name { get; set; }
    
    /// <summary>
    /// Latitude of the place
    /// </summary>
    /// <example>51.040222</example>
    public required float Lat  { get; set; }
    
    /// <summary>
    /// Longitude of the place
    /// </summary>
    /// <example>13.731409</example>
    public required float Lon { get; set; }
    
    public required List<string> Products { get; set; }
}