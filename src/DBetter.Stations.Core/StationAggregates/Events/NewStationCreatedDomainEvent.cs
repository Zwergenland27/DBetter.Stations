using CleanMessageBus.Abstractions;
using DBetter.Stations.Core.StationAggregates.ValueObjects;

namespace DBetter.Stations.Core.StationAggregates.Events;

public sealed class NewStationCreatedDomainEvent(StationId StationId): DomainEvent;