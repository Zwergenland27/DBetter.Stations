using System.Text.RegularExpressions;
using CleanDomainValidation.Domain;

namespace DBetter.Stations.Core.Common;

/// <summary>
/// Postal address (Germany). Immutable, with value equality across all four components.
/// Instances can only be created via <see cref="Create"/> and are therefore always valid.
/// </summary>
public record Address
{
    public const int StreetMaxLength = 100;
    public const int HouseNumberMaxLength = 10;
    public const int CityMaxLength = 85;
    public const int ZipCodeLength = 5;
    
    /// <summary>
    /// Unanchored pattern for a valid house number, e.g. "12", "12a", "12 a", "12-14", "12a-12c" or "12/1".
    /// </summary>
    private const string HouseNumberPattern = @"\d{1,5}\s?[a-zA-Z]?(?:\s?[-/]\s?\d{1,5}\s?[a-zA-Z]?)?";
    
    
    private static readonly Regex HouseNumberFormat = new("^" + HouseNumberPattern + "$", RegexOptions.Compiled);
    
    // The street part must end with a non-digit, so that only a trailing, whitespace-separated
    // house number is extracted.
    private static readonly Regex StreetWithHouseNumberFormat =
        new(@"^(?<street>.*\D)\s(?<houseNumber>" + HouseNumberPattern + ")$", RegexOptions.Compiled);

 
    public string Street { get; }
    public string? HouseNumber { get; }
    public string ZipCode { get; }
    public string City { get; }
    
    internal Address(string street, string? houseNumber, string zipCode, string city)
    {
        Street = street;
        HouseNumber = houseNumber;
        ZipCode = zipCode;
        City = city;
    }
    
    /// <summary>
    /// Normalizes and validates the given components and creates an <see cref="Address"/>
    /// with an explicitly given house number.
    /// All validation errors are collected rather than stopping at the first one.
    /// </summary>
    /// <param name="street">Street name, required, max. <see cref="StreetMaxLength"/> characters.</param>
    /// <param name="houseNumber">
    /// House number, required, e.g. "12", "12a", "12-14" or "12/1". Stored in lowercase.
    /// Use <see cref="Create(string?, string?, string?)"/> for addresses without a separate house number.
    /// </param>
    /// <param name="zipCode">German postal code consisting of exactly 5 digits.</param>
    /// <param name="city">City name, required, max. <see cref="CityMaxLength"/> characters.</param>
    /// <returns>The created address, or the collected validation errors.</returns>
    public static CanFail<Address> Create(string street, string houseNumber, string zipCode, string city)
    {
 
        return Validate(street, houseNumber, zipCode, city);
    }
 
    /// <summary>
    /// Normalizes and validates the given components and creates an <see cref="Address"/>
    /// without a separate house number.
    /// All validation errors are collected rather than stopping at the first one.
    /// </summary>
    /// <remarks>
    /// If the street ends with a house number, it is extracted, e.g.
    /// "Europaplatz 1" → street "Europaplatz", house number "1".
    /// Numbers within the street name are preserved:
    /// "Straße des 17. Juni 135" → street "Straße des 17. Juni", house number "135".
    /// If no house number is found (e.g. "Im Hauptbahnhof"), the address is created without one.
    /// </remarks>
    /// <param name="street">
    /// Street name, optionally followed by a house number. Required, max. <see cref="StreetMaxLength"/> characters
    /// (excluding the extracted house number).
    /// </param>
    /// <param name="zipCode">German postal code consisting of exactly 5 digits.</param>
    /// <param name="city">City name, required, max. <see cref="CityMaxLength"/> characters.</param>
    /// <returns>The created address, or the collected validation errors.</returns>
    public static CanFail<Address> Create(string street, string zipCode, string city)
    {
        var (streetName, houseNumber) = SplitHouseNumberFromStreet(street);
 
        return Validate(streetName, houseNumber, zipCode, city);
    }
    
    private static CanFail<Address> Validate(string street, string? houseNumber, string zipCode, string city)
    {
        var result = new CanFail<Address>();
        if (street.Length == 0)
        {
            result.Failed(CommonErrors.Address.StreetEmpty);
        }else if (street.Length > StreetMaxLength)
        {
            result.Failed(CommonErrors.Address.StreetTooLong);   
        }

        if (houseNumber is not null)
        {
            if (houseNumber.Length == 0)
            {
                result.Failed(CommonErrors.Address.HouseNumberEmpty);
            } else if (houseNumber.Length > HouseNumberMaxLength)
            {
                result.Failed(CommonErrors.Address.HouseNumberTooLong);
            }
        }

        if (zipCode.Length != ZipCodeLength)
        {
            result.Failed(CommonErrors.Address.ZipCodeInvalid(zipCode));   
        }

        if (city.Length == 0)
        {
            result.Failed(CommonErrors.Address.CityEmpty);   
        }else if (city.Length > CityMaxLength)
        {
            result.Failed(CommonErrors.Address.CityTooLong);   
        }
 
        if (result.HasFailed) return result;
 
        return new Address(
            street,
            houseNumber,
            zipCode,
            city);
    }
 
    /// <summary>
    /// Splits a trailing house number off a normalized street.
    /// Returns the street unchanged and an empty house number if none is found.
    /// </summary>
    private static (string Street, string? HouseNumber) SplitHouseNumberFromStreet(string street)
    {
        var match = StreetWithHouseNumberFormat.Match(street);
        if (match.Success)
        {
            return (match.Groups["street"].Value, match.Groups["houseNumber"].Value);
        }

        return (street, null);
    }
}