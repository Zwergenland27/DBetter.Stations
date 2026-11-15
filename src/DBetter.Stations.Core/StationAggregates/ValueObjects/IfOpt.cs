using System.Text.RegularExpressions;
using CleanDomainValidation.Domain;

namespace DBetter.Stations.Core.StationAggregates.ValueObjects;

/// <summary>
/// IFOPT identifier (Identification of Fixed Objects in Public Transport, DIN EN 28701)
/// of a stop place, a stop area or a quay
/// </summary>
/// <remarks>
/// In Germany also known as DHID (Deutschlandweite Haltestellen-ID).
/// Format: <c>country:region:stopPlace[:area[:quay]]</c>
/// </remarks>
/// <example>de:11000:900003201</example>
/// <example>de:08212:90:1:1</example>
public sealed record IfOpt
{
    // country:region:stopPlace[:area[:quay]]
    private static readonly Regex Format = new(
        "^[a-z]{2}(:[a-zA-Z0-9_.-]+){2,4}$",
        RegexOptions.Compiled);
    
    public string Value { get; }

    internal IfOpt(string value)
    {
        Value = value;
    }

    public static CanFail<IfOpt> Create(string value)
    {
        if (!Format.IsMatch(value))
        {
            return StationErrors.IfOpt.Invalid(value);
        }

        return new IfOpt(value);
    }
}