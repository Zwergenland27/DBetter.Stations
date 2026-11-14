namespace DB.MarketPlace.StaDa.DTOs;

internal sealed class MobilityServiceStaffDto
{
    /// <summary>
    /// Weekly schedule of the mobility service staff with up to two periods per day
    /// </summary>
    public WeeklyScheduleWithTwoRangesDto? Availability { get; set; }

    /// <summary>
    /// Preferred meeting point for mobility service assistance
    /// </summary>
    public string? MeetingPoint { get; set; }

    /// <summary>
    /// Mobility service is provided on behalf of a railway company
    /// </summary>
    /// <remarks>Advance notification is necessary</remarks>
    public bool? ServiceOnBehalf { get; set; }

    /// <summary>
    /// Staff is on site
    /// </summary>
    public bool? StaffOnSite { get; set; }
}