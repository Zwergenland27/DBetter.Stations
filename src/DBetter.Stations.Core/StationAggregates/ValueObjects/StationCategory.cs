using CleanDomainValidation.Domain;

namespace DBetter.Stations.Core.StationAggregates.ValueObjects;

/// <summary>
/// Deutsche Bahn "Bahnhofskategorie" (1 = largest / most significant stations, 7 = smallest).
/// </summary>
public record struct StationCategory(short Value)
{
    internal const int MinCategory = 1;
    internal const int MaxCategory = 7;
 
    public static CanFail<StationCategory> Create(short value)
    {
        if (value is >= MinCategory and <= MaxCategory)
        {
            return new StationCategory(value);
        }
 
        return StationErrors.Category.OutOfRange(value);
    }
}