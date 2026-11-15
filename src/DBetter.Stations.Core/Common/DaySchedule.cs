namespace DBetter.Stations.Core.Common;

public record DaySchedule
{
    public IReadOnlyList<OpeningPeriod> Periods { get; }

    private DaySchedule(IReadOnlyList<OpeningPeriod> periods)
    {
        Periods = periods;
    }
    
    public static DaySchedule Create(IEnumerable<OpeningPeriod> periods)
    {
        return new DaySchedule(periods.ToList());
    }

    internal static DaySchedule Closed()
    {
        return new DaySchedule([]);
    }
}