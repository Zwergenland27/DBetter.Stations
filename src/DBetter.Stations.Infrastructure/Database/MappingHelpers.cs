namespace DBetter.Stations.Infrastructure.Database;

public static class MappingHelpers
{
    public static void Synchronize<TPersistence, TId, TDomain>(
        this List<TPersistence> persistenceList,
        IEnumerable<TDomain> domainList,
        Func<TPersistence, TId> existingIdSelector,
        Func<TDomain, TId> domainIdSelector)
        where TPersistence: IPersistenceDto<TDomain, TPersistence>
        where TId: notnull
    {
        // Remove
        var domainObjects = domainList.ToList();
        foreach (var persistenceObject in persistenceList.ToList())
        {
            if (!domainObjects.Any(incoming =>
                {
                    var incomingId = domainIdSelector(incoming);
                    var existingId = existingIdSelector(persistenceObject);
                    return incomingId.Equals(existingId);
                }))
            {
                persistenceList.Remove(persistenceObject);
            }
        }

        // Add and refresh
        foreach (var domainObject in domainObjects)
        {
            var existingObject = persistenceList.FirstOrDefault(existing =>
            {
                var incomingId = domainIdSelector(domainObject);
                var existingId = existingIdSelector(existing);
                return incomingId.Equals(existingId);
            });
            if (existingObject is not null)
            {
                existingObject.Apply(domainObject);
            }
            else
            {
                persistenceList.Add(TPersistence.FromDomain(domainObject));
            }
        }
    }
}