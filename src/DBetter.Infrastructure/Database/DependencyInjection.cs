using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DBetter.Infrastructure.Database;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = new DatabaseSettings();
        configuration.Bind(DatabaseSettings.SectionName, settings);

        services.AddDbContext<Database>(options =>
        {
            options.UseNpgsql(settings.ConnectionString);
        });

        return services;
    }
}