using DB.MarketPlace.Timetables;
using DBetter.Stations.Core.StationAggregates.Ports.StationDetails;
using DBetter.Stations.Core.StationAggregates.ValueObjects;

namespace DBetter.Stations.Infrastructure.StationAggregates;

public class TimetableRil100Provider(TimeTableService service): IExternalRil100Provider
{
    public async Task<Ril100Code?> GetRil100Async(EvaNumber evaNumber, CancellationToken cancellationToken)
    {
        var timeTableInformation = await service.GetStationInformationAsync(evaNumber.Value.ToString(), cancellationToken);
        if (timeTableInformation is null) return null;

        var ril100Result = Ril100Code.Create(timeTableInformation.Rl100);
        if (ril100Result.HasFailed) return null;
        return ril100Result.Value;
    }
}