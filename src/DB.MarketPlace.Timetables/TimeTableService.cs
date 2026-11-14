using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Xml.Serialization;
using DB.MarketPlace.Timetables.DTOs;
using DB.MarketPlace.Timetables.Models;

namespace DB.MarketPlace.Timetables;

public class TimeTableService(HttpClient http)
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    /// <summary>
    /// Retrieves station information if available
    /// </summary>
    /// <param name="evaNumber">The eva number of the station</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>Found station information or null if no station has been found</returns>
    public async Task<Station?> GetStationInformationAsync(string evaNumber, CancellationToken cancellationToken)
    {
        using var response = await http.GetAsync($"station/{evaNumber}", cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        var serializer = new XmlSerializer(typeof(MultipleStationDataDto));

        var results = serializer.Deserialize(stream) as MultipleStationDataDto;
        
        if (results is null) throw new InvalidDataException("The response could not be parsed.");

        var match = results.Station.FirstOrDefault();
        if (match is null) return null;

        return new Station
        {
            Name = match.Name,
            Number = match.Eva.ToString(),
            Rl100 = match.Ds100
        };
    }
}