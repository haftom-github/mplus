namespace Dd.Api.Shared.Application.Repositories;

public interface IRepo<T> {
    Task<T?> GetByIdAsync<TId>(TId id) where TId : notnull;
    Task<IReadOnlyList<T>> ListAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<bool> ExistsAsync<TId>(TId id) where TId : notnull;
}