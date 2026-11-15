using DBetter.Stations.Core.Abstractions;
using DBetter.Stations.Core.StationAggregates.Features.Search.Ports;
using DBetter.Stations.Core.StationAggregates.Ports;
using DBetter.Stations.Core.StationAggregates.Ports.ExternalStationSearch;
using DBetter.Stations.Core.StationAggregates.ValueObjects;

namespace DBetter.Stations.Core.StationAggregates.Features.Search;

public class StationSearchService(
    IUnitOfWork unitOfWork,
    IInternalStationSearchService internalStationSearchService,
    IExternalStationSearchProvider externalStationSearchProvider,
    IStationRepository stationRepository)
{
    public async Task<IEnumerable<StationSearchResult>> SearchAsync(string query, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransaction(cancellationToken);
        var results = await ExternalSearchAsync(query);
        if (results.Count == 0)
        { 
            results = await InternalSearchAsync(query);   
        }
        var exactRil100Match = await SearchExactRil100Match(query);
        var exactEvaMatch = await SearchExactEvaMatch(query);

        var prioritizer = new StationSearchPrioritizer(results, exactRil100Match, exactEvaMatch);
        var ranking = prioritizer.Rank();

        await unitOfWork.CommitAsync(cancellationToken);
        
        return ranking.Select(station => new StationSearchResult
        {
            Id = station.Id,
            EvaNumber = station.EvaNumber,
            StationName = station.OfficialName,
            Rl100Code = station.Ril100Code
        });
    }

    private async Task<IReadOnlyList<StationSearchDto>> ExternalSearchAsync(string query)
    {
        try
        {
            var results = await externalStationSearchProvider.SearchAsync(query); 
            var resolvedStations = await internalStationSearchService.ResolveManyAsync(results.Select(x => x.EvaNumber));
            var existingEvas = resolvedStations.Select(existing => existing.EvaNumber);
            var missingStations = results.Where(result => !existingEvas.Contains(result.EvaNumber));
            var existingStations = resolvedStations.ToList();
            
            var createdStations = new List<Station>();
            foreach (var resultStation in missingStations)
            {
                var station = Station.Create(
                    resultStation.EvaNumber,
                    resultStation.OfficialName,
                    resultStation.Location,
                    null,
                    null,
                    null,
                    resultStation.Serves);
                
                createdStations.Add(station);
                existingStations.Add(new StationSearchDto
                {
                    Id = station.Id,
                    EvaNumber = station.EvaNumber,
                    OfficialName = station.OfficialName,
                    Ril100Code = null,
                    Category = null,
                    Serves = station.Serves
                });
            }
            
            stationRepository.SaveMany(createdStations);

            return existingStations;
        }
        catch (Exception e)
        {
            //TODO: Proper logging
            return [];
        }
    }

    private Task<StationSearchDto?> SearchExactRil100Match(string query)
    {
        var ril100Result = Ril100Code.Create(query);
        if (ril100Result.HasFailed) return Task.FromResult((StationSearchDto?)null);

        return internalStationSearchService.GetAsync(ril100Result.Value);
    }

    private Task<StationSearchDto?> SearchExactEvaMatch(string query)
    {
        var evaNumberResult = EvaNumber.Create(query);
        if (evaNumberResult.HasFailed) return Task.FromResult((StationSearchDto?)null);
        
        return internalStationSearchService.GetAsync(evaNumberResult.Value);
    }

    private Task<IReadOnlyList<StationSearchDto>> InternalSearchAsync(string query)
    {
        return internalStationSearchService.SearchAsync(query);
    }
}