using CleanDomainValidation.Domain;

namespace DBetter.Stations.Core.StationAggregates.ValueObjects;

/// <summary>
/// Name of a station
/// </summary>
/// <remarks>
/// Since the naming provided by Bahn.de is not consistent, the name will be normalized
/// </remarks>
/// <example>Dresden Hbf</example>
public record StationName
{
    public string Value { get; private init; }

    internal StationName(string value)
    {
        Value = value;
    }

    public static CanFail<StationName> Create(string rawStationName)
    {
        if (string.IsNullOrWhiteSpace(rawStationName))
        {
            return StationErrors.OfficialName.Empty;
        }
        if (IsMetaStation(rawStationName)) return StationErrors.OfficialName.MetaStation(rawStationName);
        return new StationName(rawStationName);
    }

    private static bool IsMetaStation(string value)
    {
        return value.Where(char.IsLetter).All(char.IsUpper);
    }
}