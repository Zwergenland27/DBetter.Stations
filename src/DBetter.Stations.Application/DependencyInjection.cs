using DBetter.Stations.Application.StationFeatures.Search;
using Microsoft.Extensions.DependencyInjection;

namespace DBetter.Stations.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddStationFeatures();
        return services;
    }

    private static IServiceCollection AddStationFeatures(this IServiceCollection services)
    {
        services.AddScoped<IStationSearchService, StationSearchService>();
        return services;
    }
}