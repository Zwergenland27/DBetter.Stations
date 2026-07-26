using CleanDomainValidation.Domain;

namespace DBetter.Stations.Domain.StationAggregates.ValueObjects;

public record struct StationInfoId
{
    public int Value { get; }

    internal StationInfoId(int value)
    {
        Value = value;
    }

    public static CanFail<StationInfoId> Create(string value)
    {
        if (int.TryParse(value, out var number))
        {
            return new StationInfoId(number);
        }

        return StationErrors.InfoId.InvalidFormat(value);
    }
}