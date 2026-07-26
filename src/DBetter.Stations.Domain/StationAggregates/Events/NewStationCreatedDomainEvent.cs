using CleanMessageBus.Abstractions;
using DBetter.Stations.Domain.StationAggregates.ValueObjects;

namespace DBetter.Stations.Domain.StationAggregates.Events;

public sealed class NewStationCreatedDomainEvent(StationId StationId): DomainEvent;