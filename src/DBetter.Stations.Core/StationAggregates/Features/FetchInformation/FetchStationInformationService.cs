using DBetter.Stations.Core.Abstractions;
using DBetter.Stations.Core.StationAggregates.Ports.ExternalStationSearch;
using DBetter.Stations.Core.StationAggregates.Ports.StationDetails;
using DBetter.Stations.Core.StationAggregates.ValueObjects;

namespace DBetter.Stations.Core.StationAggregates.Features.FetchInformation;

public class FetchStationInformationService(
    IUnitOfWork unitOfWork,
    IStationRepository stationRepository,
    IExternalStationSearchProvider searchProvider,
    IExternalRil100Provider ril100Provider,
    IExternalStationDetailProvider detailProvider)
{
    public async Task FetchInformation(StationId stationId, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransaction(cancellationToken);
        var stationAggregate = await stationRepository.GetAsync(stationId);
        if (stationAggregate is null) throw new InvalidOperationException("Station with this id does not exist");

        if (stationAggregate.Serves is null)
        {
            var searchProviderInformation =
                await searchProvider.FindMatchAsync(stationAggregate.OfficialName, stationAggregate.EvaNumber);
            if (searchProviderInformation is not null)
            {
                stationAggregate.Update(
                    searchProviderInformation.Location, searchProviderInformation.Serves);
            }
        }

        var ril100 = await ril100Provider.GetRil100Async(stationAggregate.EvaNumber, cancellationToken);
        if (ril100 is not null)
        {
            stationAggregate.Update(ril100);
        }

        var details = await detailProvider.GetStationDetailsAsync(stationAggregate.EvaNumber, cancellationToken);
        if (details is not null)
        {
            stationAggregate.Update(details.Category, details.IfOpt, details.InfoId, details.Details);
        }
        
        stationRepository.Save(stationAggregate);
        
        await unitOfWork.CommitAsync(cancellationToken);
    }
}