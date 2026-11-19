using CleanMessageBus.Abstractions;
using DBetter.Stations.Core.StationAggregates;
using DBetter.Stations.Core.StationAggregates.ValueObjects;
using DBetter.Stations.Infrastructure.Database;
using DBetter.Stations.Infrastructure.Messaging;
using Microsoft.EntityFrameworkCore;

namespace DBetter.Stations.Infrastructure.StationAggregates;

public class StationRepository(
    StationsContext db,
    TimeProvider time): IStationRepository
{
    public async Task<Station?> GetAsync(StationId stationId)
    {
        var dto = await db.Stations.FirstOrDefaultAsync(station => station.Id == stationId.Value);
        return dto?.ToDomain();
    }

    public void Save(Station station)
    {
        var existing = db.Stations.Local.FirstOrDefault(e => e.Id == station.Id.Value);
        if (existing is null)
        {
            db.Stations.Add(StationPersistenceDto.FromDomain(station));
        }
        else
        {
            existing.Apply(station);
        }
        
        db.OutboxMessages.AddRange(station.DomainEvents.Select(e => OutboxMessage.Create(e, time.GetUtcNow())));
    }

    public void SaveMany(IEnumerable<Station> stations)
    {
        var locals = db.Stations.Local.ToList();
        var newStations = new List<StationPersistenceDto>();
        var domainEvents = new List<IDomainEvent>();
        
        foreach (var station in stations)
        {
            var existing = locals.FirstOrDefault(e => e.Id == station.Id.Value);
            if (existing is null)
            {
                newStations.Add(StationPersistenceDto.FromDomain(station));
            }
            else
            {
                existing.Apply(station);
            }
            
            domainEvents.AddRange(station.DomainEvents);
            station.ClearDomainEvents();
        }
        
        db.Stations.AddRange(newStations);
        db.OutboxMessages.AddRange(domainEvents.Select(e => OutboxMessage.Create(e, time.GetUtcNow())));
    }
}