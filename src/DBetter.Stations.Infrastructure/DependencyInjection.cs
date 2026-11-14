using BahnDe.Stations;
using DBetter.Stations.Core.StationAggregates;
using DBetter.Stations.Core.StationAggregates.Features.Search.Ports;
using DBetter.Stations.Core.StationAggregates.Ports;
using DBetter.Stations.Infrastructure.Database;
using DBetter.Stations.Infrastructure.StationAggregates;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DBetter.Stations.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddBahnStationsApi();
        services.AddScoped<IStationRepository, StationRepository>();
        services.AddScoped<IInternalStationSearchService, InternalStationServiceRepository>();
        services.AddScoped<IExternalStationSearchProvider, StationSearchService>();
        return services;
    }
}