namespace Dd.Api.Shared.Application.Repositories;

public interface IUnitOfWork : IDisposable {
    Task<int> CompleteAsync(CancellationToken cancellationToken = default);
}