namespace DBetter.Stations.Core.StationAggregates.ValueObjects;

public enum MobilityServiceAvailability
{
    /// <summary>
    /// No mobility service at the station
    /// </summary>
    NotAvailable,

    /// <summary>
    /// Mobility service is only available with prior registration
    /// </summary>
    OnlyWithPriorRegistration,

    /// <summary>
    /// Mobility service is available, prior registration is requested
    /// </summary>
    AvailablePriorRegistrationRequested
}