namespace DB.MarketPlace.StaDa.DTOs;

internal sealed class SZentraleDto
{
    /// <summary>
    /// Unique identifier of the 3-S-Zentrale
    /// </summary>
    public int? Number { get; set; }

    /// <summary>
    /// Name of the 3-S-Zentrale
    /// </summary>
    /// <example>Berlin</example>
    public string? Name { get; set; }

    /// <summary>
    /// Address of the 3-S-Zentrale
    /// </summary>
    public AddressDto? Address { get; set; }

    /// <summary>
    /// Public phone number
    /// </summary>
    public string? PublicPhoneNumber { get; set; }

    /// <summary>
    /// Public fax number
    /// </summary>
    public string? PublicFaxNumber { get; set; }

    /// <summary>
    /// Internal phone number
    /// </summary>
    public string? InternalPhoneNumber { get; set; }

    /// <summary>
    /// Internal fax number
    /// </summary>
    public string? InternalFaxNumber { get; set; }
}