namespace DBetter.Stations.Infrastructure.Database;

public interface IPersistenceDto<TDomain, TPersistenceDto> where TPersistenceDto: IPersistenceDto<TDomain, TPersistenceDto>
{
    TDomain ToDomain();
    
    void Apply(TDomain domain);

    static abstract TPersistenceDto FromDomain(TDomain domain);
}