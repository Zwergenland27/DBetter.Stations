using DBetter.Stations.Core.StationAggregates.Features.Search;
using DBetter.Stations.Core.StationAggregates.Features.Search.Ports;
using DBetter.Stations.Core.StationAggregates.ValueObjects;
using DBetter.Stations.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace DBetter.Stations.Infrastructure.StationAggregates;

public class InternalStationServiceRepository(StationsContext db): IInternalStationSearchService
{
    public async Task<IReadOnlyList<StationSearchDto>> SearchAsync(string name)
    {
        var results = await db.Stations
            .AsNoTracking()
            .Where(station => station.OfficialName.Contains(name))
            .ToListAsync();
        return results.Select(station => new StationSearchDto
        {
            Id = new StationId(station.Id),
            EvaNumber = new EvaNumber(station.EvaNumber),
            OfficialName = new StationName(station.OfficialName),
            Ril100Code = station.Ril100Code == null ? null : new Ril100Code(station.Ril100Code),
            Category = station.StationCategory == null ? null : new StationCategory(station.StationCategory.Value),
            Serves = station.ExtractServedTransportModes()
        }).ToList();
    }

    public async Task<StationSearchDto?> GetAsync(Ril100Code ril100)
    {
        var match = await db.Stations
            .AsNoTracking()
            .FirstOrDefaultAsync(station => station.Ril100Code == ril100.Value);

        return match == null
            ? null
            : new StationSearchDto
            {
                Id = new StationId(match.Id),
                EvaNumber = new EvaNumber(match.EvaNumber),
                OfficialName = new StationName(match.OfficialName),
                Ril100Code = match.Ril100Code == null ? null : new Ril100Code(match.Ril100Code),
                Category = match.StationCategory == null ? null : new StationCategory(match.StationCategory.Value),
                Serves = match.ExtractServedTransportModes()
            };
    }

    public async Task<StationSearchDto?> GetAsync(EvaNumber evaNumber)
    {
        var match = await db.Stations
            .AsNoTracking()
            .FirstOrDefaultAsync(station => station.EvaNumber == evaNumber.Value);

        return match == null
            ? null
            : new StationSearchDto
            {
                Id = new StationId(match.Id),
                EvaNumber = new EvaNumber(match.EvaNumber),
                OfficialName = new StationName(match.OfficialName),
                Ril100Code = match.Ril100Code == null ? null : new Ril100Code(match.Ril100Code),
                Category = match.StationCategory == null ? null : new StationCategory(match.StationCategory.Value),
                Serves = match.ExtractServedTransportModes()
            };
    }

    public async Task<IReadOnlyList<StationSearchDto>> ResolveManyAsync(IEnumerable<EvaNumber> evaNumbers)
    {
        var unwrappedEvaNumbers = evaNumbers.Select(evaNumber => evaNumber.Value);
        var results = await db.Stations
            .AsNoTracking()
            .Where(station => unwrappedEvaNumbers.Contains(station.EvaNumber))
            .ToListAsync();
        return results.Select(station => new StationSearchDto
        {
            Id = new StationId(station.Id),
            EvaNumber = new EvaNumber(station.EvaNumber),
            OfficialName = new StationName(station.OfficialName),
            Ril100Code = station.Ril100Code == null ? null : new Ril100Code(station.Ril100Code),
            Category = station.StationCategory == null ? null : new StationCategory(station.StationCategory.Value),
            Serves = station.ExtractServedTransportModes()
        }).ToList();
    }
}