using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using DB.MarketPlace.StaDa.DTOs;
using DB.MarketPlace.StaDa.Mapping;
using DB.MarketPlace.StaDa.Models;

namespace DB.MarketPlace.StaDa;

public class StaDaService(HttpClient http)
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    
    /// <summary>
    /// Retrieves station information if available
    /// </summary>
    /// <param name="evaNumber">The eva number of the station</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>Found station information or null if no station has been found</returns>
    public async Task<Station?> GetStationInformationAsync(
        string evaNumber,
        CancellationToken cancellationToken)
    {
        using var response = await http.GetAsync($"stations?eva={evaNumber}", cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        var results = await response.Content.ReadFromJsonAsync<StationSearchResponseDto>(JsonOptions, cancellationToken);
        if (results is null) throw new InvalidDataException("The response could not be parsed.");

        var match = results.Result?.FirstOrDefault();
        if (match is null) return null;

        return StationMapping.Map(match);

    }
}