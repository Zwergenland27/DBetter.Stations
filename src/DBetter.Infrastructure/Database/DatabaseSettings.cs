namespace DBetter.Infrastructure.Database;

public class DatabaseSettings
{
    public const string SectionName = "Database";
    
    public string ConnectionString { get; init; }
}