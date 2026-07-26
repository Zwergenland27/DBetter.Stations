using DBetter.Stations.Infrastructure.StationAggregates;
using Microsoft.EntityFrameworkCore;

namespace DBetter.Stations.Infrastructure.Database;

public class Database(DbContextOptions<Database> options) : DbContext(options)
{
    internal DbSet<StationPersistenceDto> Stations { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Database).Assembly);
    }
}