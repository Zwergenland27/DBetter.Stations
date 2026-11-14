namespace DB.MarketPlace.StaDa.DTOs;

internal sealed class RegionalbereichDto
{
    /// <summary>
    /// Unique identifier of the regional department
    /// </summary>
    public int? Number { get; set; }

    /// <summary>
    /// Name of the regional department
    /// </summary>
    /// <example>RB Ost</example>
    public string? Name { get; set; }

    /// <summary>
    /// Short name of the regional department
    /// </summary>
    public string? ShortName { get; set; }
}