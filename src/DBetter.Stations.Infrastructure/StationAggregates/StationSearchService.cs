using BahnDe.Stations;
using DBetter.Stations.Core.StationAggregates.Ports;
using DBetter.Stations.Core.StationAggregates.Ports.ExternalStationSearch;
using DBetter.Stations.Core.StationAggregates.ValueObjects;

namespace DBetter.Stations.Infrastructure.StationAggregates;

public class StationSearchService(IStationService service): IExternalStationSearchProvider
{
    public async Task<IReadOnlyList<ExternalStationSearchDto>> SearchAsync(string query)
    {
        var stations = await service.SearchAsync(query);

        var result = new List<ExternalStationSearchDto>();
        foreach (var station in stations)
        {
            var evaNumberResult = EvaNumber.Create(station.EvaNumber);
            if (evaNumberResult.HasFailed) continue;

            var officialName = StationName.Create(station.Name);
            if (officialName.HasFailed) continue;

            var location = new Coordinates(station.Latitude, station.Longitude);
            var serves = ExtractServedTransportModes(station);
            
            result.Add(new ExternalStationSearchDto
            {
                EvaNumber = evaNumberResult.Value,
                OfficialName = officialName.Value,
                Location = location,
                Serves = serves
            });
        }

        return result;
    }

    public async Task<ExternalStationSearchDto?> FindMatchAsync(StationName officialName, EvaNumber evaNumber)
    {
        var results = await SearchAsync(officialName.Value);
        var matchByEva = results.FirstOrDefault(station => station.EvaNumber == evaNumber);
        return matchByEva;
    }

    private static ServedTransportModes ExtractServedTransportModes(Station station)
    {
        var products = station.Products;
        var servesHighSpeedTrains = products.Contains(Product.HighSpeedTrain);
        var servesFastTrains = products.Contains(Product.FastTrain);
        var servesUnpartneredFastTrains = products.Contains(Product.UnpartneredFastTrains);
        var servesRegionalTrains = products.Contains(Product.RegionalTrain);
        var servesSuburbanTrains = products.Contains(Product.SuburbanTrain);
        var servesUndergroundTrains = products.Contains(Product.UndergroundTrain);
        var servesTrams = products.Contains(Product.Tram);
        var servesBusses = products.Contains(Product.Bus);
        var servesBoats = products.Contains(Product.Boat);

        return new ServedTransportModes(
            servesHighSpeedTrains,
            servesFastTrains,
            servesUnpartneredFastTrains,
            servesRegionalTrains,
            servesSuburbanTrains,
            servesUndergroundTrains,
            servesTrams,
            servesBusses,
            servesBoats);
    }
}