namespace DB.MarketPlace.StaDa.DTOs;

internal sealed class TimeRangeDto
{
    /// <summary>
    /// Start of the period (H:mm)
    /// </summary>
    /// <example>9:00</example>
    public string? FromTime { get; set; }

    /// <summary>
    /// End of the period (H:mm)
    /// </summary>
    /// <remarks>"24:00" occurs in practice although it is not covered by the specified pattern</remarks>
    /// <example>23:00</example>
    public string? ToTime { get; set; }
}