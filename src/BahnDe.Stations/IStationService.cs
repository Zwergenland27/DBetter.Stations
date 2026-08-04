namespace BahnDe.Stations;

public interface IStationService
{
    Task<List<Station>> SearchAsync(string query, int limit = 10);
}