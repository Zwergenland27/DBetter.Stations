namespace DB.MarketPlace.StaDa.Models;

public class OpeningHours
{
    /// <summary>
    /// Opening period on Mondays
    /// </summary>
    public required List<OpeningPeriod> Monday { get; set; }

    /// <summary>
    /// Opening period on Tuesdays
    /// </summary>
    public required List<OpeningPeriod> Tuesday { get; set; }

    /// <summary>
    /// Opening period on Wednesdays
    /// </summary>
    public required List<OpeningPeriod> Wednesday { get; set; }

    /// <summary>
    /// Opening period on Thursdays
    /// </summary>
    public required List<OpeningPeriod> Thursday { get; set; }

    /// <summary>
    /// Opening period on Fridays
    /// </summary>
    public required List<OpeningPeriod> Friday { get; set; }

    /// <summary>
    /// Opening period on Saturdays
    /// </summary>
    public required List<OpeningPeriod> Saturday { get; set; }

    /// <summary>
    /// Opening period on Sundays
    /// </summary>
    public required List<OpeningPeriod> Sunday { get; set; }

    /// <summary>
    /// Opening period on public holidays
    /// </summary>
    public required List<OpeningPeriod> Holiday { get; set; }
}