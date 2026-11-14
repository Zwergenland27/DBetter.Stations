namespace DB.MarketPlace.StaDa.DTOs;

internal sealed class WeeklyScheduleWithTwoRangesDto
{
    /// <summary>
    /// First opening period on Mondays
    /// </summary>
    public TimeRangeDto? Monday1 { get; set; }

    /// <summary>
    /// Second opening period on Mondays
    /// </summary>
    public TimeRangeDto? Monday2 { get; set; }

    /// <summary>
    /// First opening period on Tuesdays
    /// </summary>
    public TimeRangeDto? Tuesday1 { get; set; }

    /// <summary>
    /// Second opening period on Tuesdays
    /// </summary>
    public TimeRangeDto? Tuesday2 { get; set; }

    /// <summary>
    /// First opening period on Wednesdays
    /// </summary>
    public TimeRangeDto? Wednesday1 { get; set; }

    /// <summary>
    /// Second opening period on Wednesdays
    /// </summary>
    public TimeRangeDto? Wednesday2 { get; set; }

    /// <summary>
    /// First opening period on Thursdays
    /// </summary>
    public TimeRangeDto? Thursday1 { get; set; }

    /// <summary>
    /// Second opening period on Thursdays
    /// </summary>
    public TimeRangeDto? Thursday2 { get; set; }

    /// <summary>
    /// First opening period on Fridays
    /// </summary>
    public TimeRangeDto? Friday1 { get; set; }

    /// <summary>
    /// Second opening period on Fridays
    /// </summary>
    public TimeRangeDto? Friday2 { get; set; }

    /// <summary>
    /// First opening period on Saturdays
    /// </summary>
    public TimeRangeDto? Saturday1 { get; set; }

    /// <summary>
    /// Second opening period on Saturdays
    /// </summary>
    public TimeRangeDto? Saturday2 { get; set; }

    /// <summary>
    /// First opening period on Sundays
    /// </summary>
    public TimeRangeDto? Sunday1 { get; set; }

    /// <summary>
    /// Second opening period on Sundays
    /// </summary>
    public TimeRangeDto? Sunday2 { get; set; }
}