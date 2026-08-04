namespace BahnDe.Stations;

public class Station
{
    /// <summary>
    /// Eva number
    /// </summary>
    /// <remarks>Only set when place is a station</remarks>
    /// <example>8010085</example>
    public required string EvaNumber { get; set; }
    
    /// <summary>
    /// Name of the place
    /// </summary>
    /// <example>Dresden Hbf</example>
    public required string Name { get; set; }
    
    /// <summary>
    /// Latitude of the place
    /// </summary>
    /// <example>51.040222</example>
    public required float Latitude  { get; set; }
    
    /// <summary>
    /// Longitude of the place
    /// </summary>
    /// <example>13.731409</example>
    public required float Longitude { get; set; }
    
    /// <summary>
    /// Available products of the station
    /// </summary>
    /// <remarks>Only set when place is a station</remarks>
    public required IReadOnlyList<Product> Products { get; set; }
}