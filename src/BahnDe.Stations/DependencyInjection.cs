using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Polly;

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
            }).AddResilienceHandler("bahn-stations", pipeline =>
            {
                pipeline.AddTimeout(TimeSpan.FromMinutes(3));

                pipeline.AddRetry(new HttpRetryStrategyOptions
                {
                    MaxRetryAttempts = 7,
                    BackoffType = DelayBackoffType.Exponential,
                    UseJitter = true,
                    Delay = TimeSpan.FromSeconds(1),
                    MaxDelay = TimeSpan.FromSeconds(30),
                    ShouldHandle = args => ValueTask.FromResult(
                        HttpClientResiliencePredicates.IsTransient(args.Outcome))
                });
            });
        return services;
    }
}