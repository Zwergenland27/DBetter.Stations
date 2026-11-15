using CleanDomainValidation.Domain;

namespace DBetter.Stations.Core.Common;

public static class CommonErrors
{
    public static class Address
    {
        public static readonly Error StreetEmpty = Error.Validation(
            "Common.Address.Street.Empty",
            "The street must not be empty.");
 
        public static readonly Error StreetTooLong = Error.Validation(
            "Common.Address.Street.TooLong",
            $"The street must not exceed {Common.Address.StreetMaxLength} characters.");
 
        public static readonly Error HouseNumberEmpty = Error.Validation(
            "Common.Address.HouseNumber.Empty",
            "The house number must not be empty.");
 
        public static Error HouseNumberTooLong => Error.Validation(
            "Common.Address.HouseNumber.InvalidFormat",
            $"The house number must not exceed {Common.Address.HouseNumberMaxLength} characters.");
 
        public static Error ZipCodeInvalid(string value) => Error.Validation(
            "Common.Address.ZipCode.InvalidFormat",
            $"{value} is no valid zip code.");
 
        public static readonly Error CityEmpty = Error.Validation(
            "Common.Address.City.Empty",
            "The city must not be empty.");
 
        public static readonly Error CityTooLong = Error.Validation(
            "Common.Address.City.TooLong",
            $"The city must not exceed {Common.Address.CityMaxLength} characters.");
    }

    public static class OpeningHours
    {
        public static Error EmptyDuration = Error.Conflict(
            "Common.OpeningHours.OpeningPeriod.EmptyDuration",
            "The opening period duration cannot be 0.");

        public static Error DurationTooLong = Error.Conflict(
            "Common.OpeningHours.OpeningPeriod.DurationTooLong",
            "The opening period duration cannot exceed 24 hours");
    }
}