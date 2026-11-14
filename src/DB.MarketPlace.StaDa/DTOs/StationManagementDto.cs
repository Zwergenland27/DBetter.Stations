namespace DB.MarketPlace.StaDa.DTOs;

internal sealed class StationManagementDto
{
    /// <summary>
    /// Identifier of the station management
    /// </summary>
    public int? Number { get; set; }

    /// <summary>
    /// Name of the station management
    /// </summary>
    /// <example>Berlin</example>
    public string? Name { get; set; }
}