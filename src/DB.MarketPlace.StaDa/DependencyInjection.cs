using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DB.MarketPlace.StaDa;

public static class DependencyInjection
{
    public static IServiceCollection AddStaDaApi(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<StaDaOptions>()
            .Bind(configuration.GetSection(StaDaOptions.SectionName))
            .ValidateOnStart();

        services.AddHttpClient<StaDaService>((provider, client) =>
        {
            var options = provider.GetRequiredService<IOptions<StaDaOptions>>().Value;

            client.BaseAddress = options.BaseAddress;
            client.DefaultRequestHeaders.Add("DB-Client-Id", options.ClientId);
            client.DefaultRequestHeaders.Add("DB-Api-Key", options.ApiKey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });
        
        return services;
    }
}