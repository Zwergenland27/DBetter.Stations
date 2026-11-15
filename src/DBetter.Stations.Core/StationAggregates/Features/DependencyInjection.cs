using DBetter.Stations.Core.StationAggregates.Features.FetchInformation;
using DBetter.Stations.Core.StationAggregates.Features.Search;
using Microsoft.Extensions.DependencyInjection;

namespace DBetter.Stations.Core.StationAggregates.Features;

internal static class DependencyInjection
{
    internal static IServiceCollection AddStationFeatures(this IServiceCollection services)
    {
        services.AddScoped<StationSearchService>();
        services.AddScoped<FetchStationInformationService>();
        return services;
    }
}