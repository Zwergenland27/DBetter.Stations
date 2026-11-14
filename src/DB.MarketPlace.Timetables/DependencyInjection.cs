using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
        });
        
        return services;
    }
}