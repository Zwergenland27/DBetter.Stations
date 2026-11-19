using BahnDe.Stations;
using CleanMessageBus.Hosting;
using CleanMessageBus.Hosting.InMemory;
using CleanMessageBus.RabbitMQ;
using DB.MarketPlace.StaDa;
using DB.MarketPlace.Timetables;
using DBetter.Stations.Core.StationAggregates;
using DBetter.Stations.Core.StationAggregates.Features.Search.Ports;
using DBetter.Stations.Core.StationAggregates.Ports;
using DBetter.Stations.Core.StationAggregates.Ports.ExternalStationSearch;
using DBetter.Stations.Core.StationAggregates.Ports.StationDetails;
using DBetter.Stations.Infrastructure.Database;
using DBetter.Stations.Infrastructure.Messaging;
using DBetter.Stations.Infrastructure.StationAggregates;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DBetter.Stations.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(TimeProvider.System);
        services.AddMessaging();
        services.AddDatabase(configuration);
        services.AddBahnStationsApi();
        services.AddStaDaApi(configuration);
        services.AddTimeTablesApi(configuration);
        services.AddScoped<IStationRepository, StationRepository>();
        services.AddScoped<IInternalStationSearchService, InternalStationServiceRepository>();
        services.AddScoped<IExternalStationSearchProvider, StationSearchService>();
        services.AddScoped<IExternalRil100Provider, TimetableRil100Provider>();
        services.AddScoped<IExternalStationDetailProvider, StadaStationDetailProvider>();
        return services;
    }
}