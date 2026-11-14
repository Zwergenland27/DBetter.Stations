namespace DB.MarketPlace.StaDa.Models;

public sealed class Address
{
    /// <summary>
    /// Street name
    /// </summary>
    /// <example>Europaplatz</example>
    public required string? Street { get; set; }

    /// <summary>
    /// House number
    /// </summary>
    /// <example>1</example>
    public required string? HouseNumber { get; set; }

    /// <summary>
    /// Zip code
    /// </summary>
    /// <example>10557</example>
    public required string? Zipcode { get; set; }

    /// <summary>
    /// City
    /// </summary>
    /// <example>Berlin</example>
    public required string? City { get; set; }
}