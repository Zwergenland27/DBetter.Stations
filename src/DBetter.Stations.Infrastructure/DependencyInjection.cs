using BahnDe.Stations;
using DBetter.Stations.Application.StationFeatures.Search.Ports;
using DBetter.Stations.Domain.StationAggregates;
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
        services.AddScoped<IStationSearchQuery, StationQueryRepository>();
        services.AddScoped<IExternalStationSearchProvider, StationSearchService>();
        return services;
    }
}