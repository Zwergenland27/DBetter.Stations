using DB.MarketPlace.StaDa.Models;

namespace DB.MarketPlace.StaDa.Mapping;

internal class MobilityServiceMapping
{
    // The free text contains a phone number which may change over time,
    // so the prefix is used as a fallback to stay robust against such changes.
    private static readonly Dictionary<string, MobilityServiceAvailability> Prefixes =
        new()
        {
            {"no", MobilityServiceAvailability.NotAvailable},
            {"Nur nach Voranmeldung", MobilityServiceAvailability.OnlyWithPriorRegistration},
            {"Ja", MobilityServiceAvailability.AvailablePriorRegistrationRequested}
        };

    internal static MobilityServiceAvailability Map(string? value)
    {
        if (value is null)
        {
            return MobilityServiceAvailability.NotAvailable;
        }

        foreach (var (prefix, availability) in Prefixes)
        {
            if (value.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                return availability;
            }
        }

        throw new InvalidOperationException($"Mobility service value {value} is not supported");
    }
}