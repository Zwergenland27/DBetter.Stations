using CleanMessageBus.Hosting;
using CleanMessageBus.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;

namespace DBetter.Stations.Infrastructure.Messaging;

public static class DependencyInjection
{
    public static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        services.AddCleanMessageBus(cfg =>
        {
            cfg.ApplicationName = "DBetter.Stations";
            cfg.UseRabbitMq();
        });

        services.AddHostedService<OutboxProcessor>();
        
        return services;
    }
}