using CleanMessageBus.Abstractions;
using DBetter.Stations.Core.StationAggregates.ValueObjects;

namespace DBetter.Stations.Core.StationAggregates.Events;

public sealed record NewStationCreatedDomainEvent(StationId StationId): IDomainEvent;