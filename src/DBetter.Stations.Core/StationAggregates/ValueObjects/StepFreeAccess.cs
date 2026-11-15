namespace DBetter.Stations.Core.StationAggregates.ValueObjects;

public enum StepFreeAccess
{
    /// <summary>
    /// The station is completely accessible without steps
    /// </summary>
    Yes,

    /// <summary>
    /// The station is not accessible without steps
    /// </summary>
    No,

    /// <summary>
    /// Only parts of the station are accessible without steps
    /// </summary>
    Partial
}