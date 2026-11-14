using System.Globalization;
using DB.MarketPlace.StaDa.DTOs;
using DB.MarketPlace.StaDa.Models;

namespace DB.MarketPlace.StaDa.Mapping;

internal static class ScheduleMapping
{
    private static readonly string[] TimeFormats = ["H:mm", "HH:mm"];
    
    internal static OpeningHours? Map(this WeeklyScheduleDto? dto)
    {
        if (dto is null)
        {
            return null;
        }
 
        return new OpeningHours
        {
            Monday = MapRanges(dto.Monday),
            Tuesday = MapRanges(dto.Tuesday),
            Wednesday = MapRanges(dto.Wednesday),
            Thursday = MapRanges(dto.Thursday),
            Friday = MapRanges(dto.Friday),
            Saturday = MapRanges(dto.Saturday),
            Sunday = MapRanges(dto.Sunday),
            Holiday = MapRanges(dto.Holiday)
        };
    }
 
    internal static OpeningHours? Map(this WeeklyScheduleWithTwoRangesDto? dto)
    {
        if (dto is null)
        {
            return null;
        }
 
        return new OpeningHours
        {
            Monday = MapRanges(dto.Monday1, dto.Monday2),
            Tuesday = MapRanges(dto.Tuesday1, dto.Tuesday2),
            Wednesday = MapRanges(dto.Wednesday1, dto.Wednesday2),
            Thursday = MapRanges(dto.Thursday1, dto.Thursday2),
            Friday = MapRanges(dto.Friday1, dto.Friday2),
            Saturday = MapRanges(dto.Saturday1, dto.Saturday2),
            Sunday = MapRanges(dto.Sunday1, dto.Sunday2),
            Holiday = []
        };
    }
 
    private static List<OpeningPeriod> MapRanges(params TimeRangeDto?[] ranges)
    {
        return ranges
            .Select(MapRange)
            .OfType<OpeningPeriod>()
            .ToList();
    }
 
    private static OpeningPeriod? MapRange(TimeRangeDto? range)
    {
        var fromTime = range?.FromTime.MapTime();
        var toTime = range?.ToTime.MapTime();
        if (fromTime is null || toTime is null)
        {
            return null;
        }
 
        return new OpeningPeriod
        {
            FromTime = fromTime.Value,
            ToTime = toTime.Value
        };
    }
    
    /// <summary>
    /// Parses a StaDa time ("9:00", "09:00", "24:00") into a <see cref="TimeOnly"/>.
    /// </summary>
    /// <remarks>
    /// "24:00" is not covered by the spec pattern but used in practice for "until end of day"
    /// and is mapped to <see cref="TimeOnly.MaxValue"/>. Empty values are mapped to <c>null</c>.
    /// </remarks>
    private static TimeOnly? MapTime(this string? time)
    {
        if (string.IsNullOrWhiteSpace(time))
        {
            return null;
        }
 
        if (time.StartsWith("24:"))
        {
            return TimeOnly.MaxValue;
        }
 
        if (TimeOnly.TryParseExact(time, TimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
        {
            return result;
        }
 
        throw new InvalidOperationException($"Time {time} is not supported");
    }
}