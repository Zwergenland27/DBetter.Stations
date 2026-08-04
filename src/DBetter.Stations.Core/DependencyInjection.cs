using DBetter.Stations.Core.StationAggregates.Features;
using Microsoft.Extensions.DependencyInjection;

namespace DBetter.Stations.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddStationFeatures();
        return services;
    }
}