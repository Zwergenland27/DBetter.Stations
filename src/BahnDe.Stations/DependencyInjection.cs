using System.Net;
using Microsoft.Extensions.DependencyInjection;

namespace BahnDe.Stations;

public static class DependencyInjection
{
    public static IServiceCollection AddBahnStationsApi(this IServiceCollection services)
    {
        services.AddHttpClient<IStationService, StationService>(client =>
                client.BaseAddress = new Uri("https://www.bahn.de/web/api/"))
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip
            });
        return services;
    }
}