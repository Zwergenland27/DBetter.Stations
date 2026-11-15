using DBetter.Stations.Core.Common;

namespace DBetter.Stations.Core.StationAggregates.ValueObjects;

public record StationDetails
{
    public required StepFreeAccess? StepFreeAccess { get; init; }
    
    public required MobilityServiceAvailability? MobilityServiceAvailability { get; init; }
    
    public required Facilities? Facilities { get; init; }
    
    public required OpeningHours? DbInformation { get; init; }
    
    public required OpeningHours? LocalServiceStaff { get; init; }
    
    public required OpeningHours? MobilityServiceStaff { get; init; }
    
    public required Address? Address { get; init; }
}