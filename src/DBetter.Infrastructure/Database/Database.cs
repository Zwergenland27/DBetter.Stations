using Microsoft.EntityFrameworkCore;

namespace DBetter.Infrastructure.Database;

public class Database(DbContextOptions<Database> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Database).Assembly);
    }
}