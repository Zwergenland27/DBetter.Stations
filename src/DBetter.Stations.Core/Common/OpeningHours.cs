namespace DBetter.Stations.Core.Common;

public record OpeningHours
{
    private readonly Dictionary<DayOfWeek, DaySchedule> _weekDays;
    
    public DaySchedule? PublicHolidays { get; }

    public IReadOnlyDictionary<DayOfWeek, DaySchedule> WeekDays => _weekDays.AsReadOnly();

    internal OpeningHours(Dictionary<DayOfWeek, DaySchedule> weekdays, DaySchedule? publicHolidays)
    {
        _weekDays = weekdays;
        PublicHolidays = publicHolidays;
    }

    public static OpeningHours Create(
        DaySchedule? mondaySchedule,
        DaySchedule? tuesdaySchedule,
        DaySchedule? wednesdaySchedule,
        DaySchedule? thursdaySchedule,
        DaySchedule? fridaySchedule,
        DaySchedule? saturdaySchedule,
        DaySchedule? sundaySchedule,
        DaySchedule? holidaySchedule)
    {
        var openingHours = new Dictionary<DayOfWeek, DaySchedule>
        {
            [DayOfWeek.Monday] = mondaySchedule ?? DaySchedule.Closed(),
            [DayOfWeek.Tuesday] = tuesdaySchedule ?? DaySchedule.Closed(),
            [DayOfWeek.Wednesday] = wednesdaySchedule ?? DaySchedule.Closed(),
            [DayOfWeek.Thursday] = thursdaySchedule ?? DaySchedule.Closed(),
            [DayOfWeek.Friday] = fridaySchedule ?? DaySchedule.Closed(),
            [DayOfWeek.Saturday] = saturdaySchedule ?? DaySchedule.Closed(),
            [DayOfWeek.Sunday] = sundaySchedule ?? DaySchedule.Closed()
        };

        return new OpeningHours(openingHours, holidaySchedule);
    }
}