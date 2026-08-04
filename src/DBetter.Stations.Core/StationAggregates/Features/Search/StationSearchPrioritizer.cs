using DBetter.Stations.Core.StationAggregates.ValueObjects;

namespace DBetter.Stations.Core.StationAggregates.Features.Search;

public class StationSearchPrioritizer(
    IEnumerable<StationSearchDto> allResults,
    StationSearchDto? exactRil100Match,
    StationSearchDto? exactEvaMatch)
{
    public IReadOnlyList<StationSearchDto> Rank(int limit = 10)
    {
        var merged = Merge();

        var considerCategory = merged.All(station => station.Category is not null);
        var considerServices = merged.All(station => station.Serves is not null);

        var ordered = merged.OrderByDescending(IsExactMatch);
        if (considerCategory)
        {
            ordered = ordered.ThenBy(s => s.Category);
        }

        if (considerServices)
        {
            ordered = ordered.ThenByDescending(s => TransportModeScore(s.Serves));
        }

        return ordered
            .Take(limit)
            .ToList();
    }
    
    private List<StationSearchDto> Merge()
    {
        var merged = new List<StationSearchDto>();
        merged.AddRange(allResults);
        if (exactRil100Match is not null && merged.All(station => station.Id != exactRil100Match.Id))
        {
            merged.Add(exactRil100Match);
        }

        if (exactEvaMatch is not null && merged.All(station => station.Id != exactEvaMatch.Id))
        {
            merged.Add(exactEvaMatch);
        }
        
        return merged;
    }

    private bool IsExactMatch(StationSearchDto station)
    {
        return station.Id == exactRil100Match?.Id || station.Id == exactEvaMatch?.Id;
    }
    
    private static int TransportModeScore(ServedTransportModes? modes)
    {
        if (modes is null) return -1;

        var score = 0;
        if (modes.ServesHighSpeedTrains) score += 256;
        if (modes.ServesFastTrains) score += 128;
        if (modes.ServesUnpartneredFastTrains) score += 64;
        if (modes.ServesRegionalTrains) score += 32;
        if (modes.ServesSuburbanTrains) score += 16;
        if (modes.ServesUndergroundTrains) score += 8;
        if (modes.ServesTrams) score += 4;
        if (modes.ServesBusses) score += 2;
        if (modes.ServesBoats) score += 1;
        return score;
    }
}