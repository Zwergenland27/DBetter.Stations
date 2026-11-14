namespace DB.MarketPlace.StaDa;

public class StaDaOptions
{
    public const string SectionName = "StaDa";

    public Uri BaseAddress { get; set; } = new("https://apis.deutschebahn.com/db-api-marketplace/apis/station-data/v2/");

    public required string ClientId { get; set; }

    public required string ApiKey { get; set; }
}