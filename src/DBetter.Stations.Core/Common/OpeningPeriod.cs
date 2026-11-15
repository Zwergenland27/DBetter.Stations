using CleanDomainValidation.Domain;

namespace DBetter.Stations.Core.Common;

public sealed record OpeningPeriod
{
    public TimeOnly Opens { get; }
    
    public TimeSpan Duration { get; }

    private OpeningPeriod(TimeOnly opens, TimeSpan duration)
    {
        Opens = opens;
        Duration = duration;
    }

    public static CanFail<OpeningPeriod> Create(TimeOnly opens, TimeSpan duration)
    {
        if (duration <= TimeSpan.Zero) return CommonErrors.OpeningHours.EmptyDuration;
        if (duration > TimeSpan.FromDays(1)) return CommonErrors.OpeningHours.DurationTooLong;
        return new OpeningPeriod(opens, duration);
    }

    public static CanFail<OpeningPeriod> Create(TimeOnly from, TimeOnly to)
    {
        var duration = to.ToTimeSpan() - from.ToTimeSpan();
        return Create(from, duration <= TimeSpan.Zero ? duration + TimeSpan.FromDays(1) : duration);
    }
}