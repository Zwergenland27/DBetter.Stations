using System.Net.Http.Json;

namespace BahnDe.Stations;

public class StationService(HttpClient http): IStationService
{
    public async Task<List<Station>> SearchAsync(string query, int limit = 10)
    {
        try
        {
            var orte =
                await http.GetFromJsonAsync<List<Ort>>($"reiseloesung/orte?suchbegriff={query}&limit={limit}");

            if (orte is null) throw new InvalidOperationException("No station data received");

            return orte
                .Where(ort => ort.ExtId is not null)
                .Select(ort => new Station
                {
                    Products = ProductMapping.MapProducts(ort.Products),
                    EvaNumber = ort.ExtId!,
                    Name = ort.Name,
                    Latitude = ort.Lat,
                    Longitude = ort.Lon,
                })
                .ToList();
        }
        catch (HttpRequestException httpException)
        {
            throw new InvalidOperationException($"Instead of success the station API returned {httpException.StatusCode}");
        }
    }
}