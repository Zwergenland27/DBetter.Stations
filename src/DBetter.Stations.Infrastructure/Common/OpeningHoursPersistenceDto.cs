using System.Text.Json;
using DBetter.Stations.Core.Common;

namespace DBetter.Stations.Infrastructure.Common;

public class OpeningHoursPersistenceDto
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    
    public int Version { get; init; } = 1;
 
    public required IReadOnlyDictionary<DayOfWeek, IReadOnlyList<OpeningPeriodPersistenceDto>> WeekDays { get; init; }
 
    public IReadOnlyList<OpeningPeriodPersistenceDto>? PublicHolidays { get; init; }
    
    public static OpeningHoursPersistenceDto? FromDomain(OpeningHours? domain)
    {
        if (domain is null) return null;
 
        return new OpeningHoursPersistenceDto
        {
            WeekDays = domain.WeekDays.ToDictionary(
                day => day.Key,
                day => FromSchedule(day.Value)),
            PublicHolidays = domain.PublicHolidays is not null ? FromSchedule(domain.PublicHolidays) : null,
        };
    }
 
    public OpeningHours ToDomain()
    {
        return new OpeningHours(
            WeekDays.ToDictionary(
                day => day.Key,
                day => ToSchedule(day.Value)),
            PublicHolidays is not null ? ToSchedule(PublicHolidays) : null);
    }
 
    private static IReadOnlyList<OpeningPeriodPersistenceDto> FromSchedule(DaySchedule schedule)
    {
        return schedule.Periods
            .Select(period => new OpeningPeriodPersistenceDto
            {
                Opens = period.Opens,
                Duration = period.Duration
            })
            .ToList();
    }
 
    private static DaySchedule ToSchedule(IReadOnlyList<OpeningPeriodPersistenceDto> periods)
    {
        // .Value wirft ValueInvalidException, falls ungültige Daten in der DB stehen
        return DaySchedule.Create(periods.Select(period =>
            OpeningPeriod.Create(period.Opens, period.Duration).Value));
    }
    
    public static string? Serialize(OpeningHours? domain)
    {
        return domain is null ? null : JsonSerializer.Serialize(FromDomain(domain), JsonOptions);
    }

    public static OpeningHours? Deserialize(string? json)
    {
        return json is null
            ? null
            : JsonSerializer.Deserialize<OpeningHoursPersistenceDto>(json, JsonOptions)!.ToDomain();
    }
}