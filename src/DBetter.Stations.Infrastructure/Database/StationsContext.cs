using DBetter.Stations.Infrastructure.StationAggregates;
using Microsoft.EntityFrameworkCore;

namespace DBetter.Stations.Infrastructure.Database;

public class StationsContext(DbContextOptions<StationsContext> options) : DbContext(options)
{
    internal DbSet<StationPersistenceDto> Stations { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StationsContext).Assembly);
    }
}