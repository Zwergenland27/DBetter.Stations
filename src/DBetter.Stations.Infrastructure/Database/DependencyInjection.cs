using DBetter.Stations.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DBetter.Stations.Infrastructure.Database;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = new DatabaseSettings();
        configuration.Bind(DatabaseSettings.SectionName, settings);

        services.AddDbContext<StationsContext>(options =>
        {
            options.UseNpgsql(settings.ConnectionString);
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}