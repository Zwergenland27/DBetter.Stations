namespace DB.MarketPlace.StaDa.Models;

public class OpeningPeriod
{
    /// <summary>
    /// Start of the period
    /// </summary>
    public required TimeOnly FromTime { get; set; }

    /// <summary>
    /// End of the period
    /// </summary>
    public required TimeOnly ToTime { get; set; }
}