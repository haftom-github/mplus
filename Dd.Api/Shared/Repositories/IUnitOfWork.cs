namespace Dd.Api.Shared.Repositories;

public interface IUnitOfWork : IDisposable {
    Task<int> CompleteAsync(CancellationToken cancellationToken = default);
}