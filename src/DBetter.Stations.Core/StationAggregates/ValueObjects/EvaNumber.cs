using CleanDomainValidation.Domain;

namespace DBetter.Stations.Core.StationAggregates.ValueObjects;

/// <summary>
/// EVA number of the station
/// </summary>
/// <remarks>Also known as ibnr number</remarks>
/// <example>8010085</example>
public record struct EvaNumber
{
    public int Value { get; init; }

    internal EvaNumber(int value)
    {
        Value = value;
    }

    public static CanFail<EvaNumber> Create(string rawEva)
    {
        if (string.IsNullOrWhiteSpace(rawEva))
        {
            return StationErrors.EvaNumber.Empty;
        }
        if (rawEva.StartsWith("@L="))
        {
            rawEva = rawEva.Substring(3);
        }
        if (int.TryParse(rawEva, out var number))
        {
            return new EvaNumber(number);
        }

        return StationErrors.EvaNumber.Invalid(rawEva);
    }

    public string AsFuzzy()
    {
        return $"@L={Value}";
    }
}