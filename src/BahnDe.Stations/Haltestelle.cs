namespace BahnDe.Stations;

internal class Haltestelle
{
    /// <summary>
    /// Eva number
    /// </summary>
    /// <remarks>Only set when place is a station</remarks>
    /// <example>8010085</example>
    public required string ExtId { get; set; }
    
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
    
    /// <summary>
    /// Available products of the station
    /// </summary>
    /// <remarks>Only set when place is a station</remarks>
    public required List<string> Products { get; set; }
}