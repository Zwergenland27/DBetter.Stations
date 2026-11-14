namespace DB.MarketPlace.StaDa.DTOs;

internal sealed class LocalServiceStaffDto
{
    /// <summary>
    /// Weekly schedule of the local service staff
    /// </summary>
    public WeeklyScheduleDto? Availability { get; set; }
}