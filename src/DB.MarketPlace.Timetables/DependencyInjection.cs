using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Options;
using Polly;

namespace DB.MarketPlace.Timetables;

public static class DependencyInjection
{
    public static IServiceCollection AddTimeTablesApi(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<TimeTableOptions>()
            .Bind(configuration.GetSection(TimeTableOptions.SectionName))
            .ValidateOnStart();

        services.AddHttpClient<TimeTableService>((provider, client) =>
        {
            var options = provider.GetRequiredService<IOptions<TimeTableOptions>>().Value;

            client.BaseAddress = options.BaseAddress;
            client.DefaultRequestHeaders.Add("DB-Client-Id", options.ClientId);
            client.DefaultRequestHeaders.Add("DB-Api-Key", options.ApiKey);
            client.DefaultRequestHeaders.Add("Accept", "application/xml");
        }).AddResilienceHandler("timetables", pipeline =>
        {
            pipeline.AddTimeout(TimeSpan.FromMinutes(3));

            pipeline.AddRetry(new HttpRetryStrategyOptions
            {
                MaxRetryAttempts = 7,
                BackoffType = DelayBackoffType.Exponential,
                UseJitter = true,
                Delay = TimeSpan.FromSeconds(2),
                MaxDelay = TimeSpan.FromSeconds(30),
                ShouldHandle = args => ValueTask.FromResult(
                    HttpClientResiliencePredicates.IsTransient(args.Outcome) ||
                    IsQuotaExhausted(args.Outcome.Result))
            });
        });
        
        return services;
    }
    
    private static bool IsQuotaExhausted(HttpResponseMessage? response) =>
        response is not null
        && response.Headers.TryGetValues("x-ratelimit-remaining", out var values)
        && values.Any(v => v.Trim().EndsWith(",0;"));
}