namespace DB.MarketPlace.Timetables;

public class TimeTableOptions
{
    public const string SectionName = "TimeTable";

    public Uri BaseAddress { get; set; } = new("https://apis.deutschebahn.com/db-api-marketplace/apis/timetables/v1/");

    public required string ClientId { get; set; }

    public required string ApiKey { get; set; }
}