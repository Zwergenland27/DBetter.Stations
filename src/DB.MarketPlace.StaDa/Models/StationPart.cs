namespace DB.MarketPlace.StaDa.Models;

public class StationPart
{
    /// <summary>
    /// EVA identifier of the station part
    /// </summary>
    /// <example>8011160</example>
    public required string Number { get; set; }

    /// <summary>
    /// Whether this is the main EVA number of the station
    /// </summary>
    /// <remarks>Supported for compatibility reasons only, has no business background in terms of DB InfraGO AG</remarks>
    public bool? IsMain { get; set; }
}