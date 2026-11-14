namespace DB.MarketPlace.StaDa.DTOs;

internal sealed class WeeklyScheduleDto
{
    /// <summary>
    /// Opening period on Mondays
    /// </summary>
    public TimeRangeDto? Monday { get; set; }

    /// <summary>
    /// Opening period on Tuesdays
    /// </summary>
    public TimeRangeDto? Tuesday { get; set; }

    /// <summary>
    /// Opening period on Wednesdays
    /// </summary>
    public TimeRangeDto? Wednesday { get; set; }

    /// <summary>
    /// Opening period on Thursdays
    /// </summary>
    public TimeRangeDto? Thursday { get; set; }

    /// <summary>
    /// Opening period on Fridays
    /// </summary>
    public TimeRangeDto? Friday { get; set; }

    /// <summary>
    /// Opening period on Saturdays
    /// </summary>
    public TimeRangeDto? Saturday { get; set; }

    /// <summary>
    /// Opening period on Sundays
    /// </summary>
    public TimeRangeDto? Sunday { get; set; }

    /// <summary>
    /// Opening period on public holidays
    /// </summary>
    public TimeRangeDto? Holiday { get; set; }
}