using CleanMessageBus.Hosting;
using DB.MarketPlace.StaDa;
using DB.MarketPlace.Timetables;
using DBetter.Stations.Core.StationAggregates.Events;
using DBetter.Stations.Core.StationAggregates.Features.FetchInformation;
using DBetter.Stations.Core.StationAggregates.ValueObjects;

namespace DBetter.Stations.WebApi.StationFeatures.FetchInformation;

/// <summary>
/// Handles domain event after station has been created
/// </summary>
public static class StationCreatedDomainEvent
{
    public static WebApplication AddFetchStationInformation(this WebApplication app)
    {
        app.MapDomainEventHandler<NewStationCreatedDomainEvent>(async (
            NewStationCreatedDomainEvent @event,
            FetchStationInformationService service,
            CancellationToken ct) =>
        {
            await service.FetchInformation(@event.StationId, ct);
        });

        app.MapPost("stations/{id}/actions/fetch-information",
                async (string id, FetchStationInformationService service, CancellationToken ct) =>
                {
                    var stationIdCreationResult = StationId.Create(id);
                    if (stationIdCreationResult.HasFailed) return Results.BadRequest();

                    await service.FetchInformation(stationIdCreationResult.Value, ct);
                    return Results.Ok();
                })
            .WithName("FetchStationInformation")
            .WithSummary("Fetch additional information for a station")
            .WithDescription("""
                             Fetch additional information about the provided station id from multiple different APIs.
                             This is endpoint should only be used for administrative work as the service is automatically triggered
                             after creation a new station.
                             """)
            .WithTags("Stations", "Administrative");
        
        return app;
    }
}