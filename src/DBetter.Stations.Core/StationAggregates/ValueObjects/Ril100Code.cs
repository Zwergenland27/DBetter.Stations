using CleanDomainValidation.Domain;

namespace DBetter.Stations.Core.StationAggregates.ValueObjects;

public record Ril100Code
{
    internal const int MinLength = 2;
    internal const int MaxLength = 5;
    public string Value { get; private init; }

    internal Ril100Code(string value)
    {
        Value = value;
    }
    
    public static CanFail<Ril100Code> Create(string value)
    {
        value = value.Trim().ToUpperInvariant();
        if (!value.All(c => char.IsLetter(c) || c is ' '))
        {
            return StationErrors.Ril100Code.InvalidFormat(value);
        }
        var length =  value.Length;
        if (length is < MinLength or > MaxLength)
        {
            return StationErrors.Ril100Code.InvalidLength(value);
        }
        return new Ril100Code(value);
    }
}