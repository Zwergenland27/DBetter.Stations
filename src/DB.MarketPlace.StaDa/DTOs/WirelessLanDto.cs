namespace DB.MarketPlace.StaDa.DTOs;

internal sealed class WirelessLanDto
{
    /// <summary>
    /// Number of access points
    /// </summary>
    /// <example>32</example>
    public long? Amount { get; set; }

    /// <summary>
    /// Installation date
    /// </summary>
    public string? InstallDate { get; set; }

    /// <summary>
    /// Product name of the installation
    /// </summary>
    public string? Product { get; set; }
}