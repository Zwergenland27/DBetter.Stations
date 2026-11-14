namespace DB.MarketPlace.StaDa.DTOs;

internal sealed class AddressDto
{
    /// <summary>
    /// Street name
    /// </summary>
    /// <example>Europaplatz</example>
    public string? Street { get; set; }

    /// <summary>
    /// House number
    /// </summary>
    /// <example>1</example>
    public string? HouseNumber { get; set; }

    /// <summary>
    /// Zip code
    /// </summary>
    /// <example>10557</example>
    public string? Zipcode { get; set; }

    /// <summary>
    /// City
    /// </summary>
    /// <example>Berlin</example>
    public string? City { get; set; }
}