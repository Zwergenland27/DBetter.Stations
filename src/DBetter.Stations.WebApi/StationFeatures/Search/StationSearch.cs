using System.ComponentModel;
using DBetter.Stations.Core.StationAggregates.Features.Search;
using Microsoft.AspNetCore.Mvc;

namespace DBetter.Stations.WebApi.StationFeatures.Search;

public static class StationSearch
{
    public static IEndpointRouteBuilder AddStationSearch(this IEndpointRouteBuilder app)
    {
        app.MapGet("stations", async (
                [FromQuery(Name = "query")]
                [Description("The term to search for. Also supports ril100 identifiers and eva numbers if already known in the system")]
                string query,
                StationSearchService stationService,
                CancellationToken ct) =>
            {
                var results = await stationService.SearchAsync(query, ct);
                return Results.Ok(results.Select(r => new StationSearchResponse
                {
                    Id = r.Id.Value.ToString(),
                    EvaNumber = r.EvaNumber.Value,
                    StationName = r.StationName.Value,
                    Rl100Code = r.Rl100Code?.Value
                }));
            })
            .WithName("SearchStations")
            .WithSummary("Search for public transport stations")
            .WithDescription("""
                             Search for public transport stations (rail, tram, bus, etc.) using the station name
                             based on the official station name from bahn.de. Also supports exact matches for known
                             EVA numbers and ril identifiers
                             """)
            .WithTags("Stations")
            .Produces<List<StationSearchResponse>>();
        return app;
    }
}