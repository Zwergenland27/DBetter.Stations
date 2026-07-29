namespace DBetter.Stations.Application.Abstractions;

public interface IUnitOfWork : IDisposable
{
    Task BeginTransaction(CancellationToken cancellationToken = default);
    
    Task CommitAsync(CancellationToken cancellationToken = default);
    
    Task AbortAsync(CancellationToken cancellationToken = default);
}