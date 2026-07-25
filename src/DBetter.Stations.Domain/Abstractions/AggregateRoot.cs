using CleanMessageBus.Abstractions;

namespace DBetter.Stations.Domain.Abstractions;

public class AggregateRoot<TId> : Entity<TId>
    where TId: notnull
{
    private readonly List<DomainEvent> _domainEvents = [];
    
    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents.ToList().AsReadOnly();
    
    protected AggregateRoot(TId id) : base(id)
    {
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void RaiseDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}