namespace DB.MarketPlace.StaDa.DTOs;

internal sealed class Ril100IdentifierDto
{
    /// <summary>
    /// Unique identifier of the 'Betriebsstelle' according to Ril100
    /// </summary>
    /// <example>BLS</example>
    public required string RilIdentifier { get; set; }

    /// <summary>
    /// Whether this is the main Ril100 identifier of the station
    /// </summary>
    /// <remarks>Determined by DB InfraGO AG</remarks>
    public bool? IsMain { get; set; }

    /// <summary>
    /// UIC Primary Location Code (PLC)
    /// </summary>
    public string? PrimaryLocationCode { get; set; }

    /// <summary>
    /// Restriction for the entry of steam engines
    /// </summary>
    /// <remarks>Known values are 'restricted', 'unrestricted' and 'entryBan'</remarks>
    /// <example>entryBan</example>
    public string? SteamPermission { get; set; }

    /// <summary>
    /// Position of the Betriebsstelle as GeoJSON point
    /// </summary>
    public GeoJsonPointDto? GeographicCoordinates { get; set; }
}