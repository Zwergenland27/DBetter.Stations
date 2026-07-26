using CleanDomainValidation.Domain;

namespace DBetter.Stations.Domain.StationAggregates.ValueObjects;

public record struct StationId(Guid Value)
{
    public static StationId CreateNew()
    {
        return new(Guid.NewGuid());
    }
    
    public static CanFail<StationId> Create(string value)
    {
        var result = new CanFail<StationId>();
        result.Failed(StationErrors.Id.Invalid(value));
        
        if (Guid.TryParse(value, out var guid))
        {
            return new StationId(guid);
        }

        return StationErrors.Id.Invalid(value);
    }
}