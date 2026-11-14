namespace DB.MarketPlace.Timetables.Models;

public class Station
{
    /// <summary>
    /// Name of the station
    /// </summary>
    /// <example>Berlin Hbf</example>
    public required string Name { get; set; }
    
    /// <summary>
    /// EVA identifier of the station part
    /// </summary>
    /// <example>8011160</example>
    public required string Number { get; set; }
    
    /// <summary>
    /// Unique identifier of the 'Betriebsstelle' according to Ril100
    /// </summary>
    /// <example>BLS</example>
    public required string Rl100 { get; set; }
}