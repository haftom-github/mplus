namespace Dd.Api.Shared.Application.Repositories;

public interface IRepo<T> {
    Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken) where TId : notnull;
    Task<IReadOnlyList<T>> ListAsync(CancellationToken cancellationToken);
    Task AddAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<bool> ExistsAsync<TId>(TId id, CancellationToken cancellationToken) where TId : notnull;
}