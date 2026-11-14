using DB.MarketPlace.StaDa.Models;

namespace DB.MarketPlace.StaDa.Mapping;

internal class StepFreeAccessMapping
{
    private static readonly Dictionary<string, StepFreeAccess> Values =
        new()
        {
            {"yes", StepFreeAccess.Yes},
            {"no", StepFreeAccess.No},
            {"partial", StepFreeAccess.Partial}
        };

    internal static StepFreeAccess Map(string? value)
    {
        if (value is null)
        {
            return StepFreeAccess.Unknown;
        }

        if (Values.TryGetValue(value, out var result))
        {
            return result;
        }

        throw new InvalidOperationException($"Stepless access value {value} is not supported");
    }
}