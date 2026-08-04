using CleanDomainValidation.Domain;
using DBetter.Stations.Core.StationAggregates.ValueObjects;

namespace DBetter.Stations.Core.StationAggregates;

public static class StationErrors
{
    public static class Id
    {
        public static Error Invalid(string value) => Error.Validation("Station.Id.Invalid", $"'{value}' is no valid guid.");
    }

    public static class EvaNumber
    {
        public static Error Empty => Error.Validation("Station.Eva.Empty", "The eva number cannot be empty.");
        public static Error Invalid(string value) =>
            Error.Validation("Station.Eva.Invalid", $"'{value}' is no valid eva number.");
    }

    public static class OfficialName
    {
        public static Error Empty => Error.Validation("Station.Name.Empty", "The station name cannot be empty.");
        public static Error MetaStation(string value) =>
            Error.Conflict("Station.Name.MetaStation", $"Metastation '{value}' is no real station.");
    }

    public static class Ril100Code
    {
        public static Error InvalidFormat(string value) => Error.Validation("Station.Ril100.InvalidFormat", $"The ril100 '{value}' must only contain letters and spaces.");
        
        public static Error InvalidLength(string value) => Error.Validation("Station.Ril100.InvalidLength", $"The ril100 '{value}' must be between 2 and 5 characters long.");
    }

    public static class Category
    {
        public static Error OutOfRange(int value) => Error.Validation("Station.Category.OutOfRange",
            $"The station category '{value}' must be between 1 (largest) and 7 (smallest).");
    }

    public static class InfoId
    {
        public static Error InvalidFormat(string value) => Error.Validation("Station.InfoId.InvalidFormat",
            $"The station info id '{value}' must only contain numbers");
    }
}