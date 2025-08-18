using Dd.Api.Shared.Results;

namespace Dd.Api.Shared.Repositories;

public interface IRepo<T> {
    Task<Result<T>> GetByIdAsync<TId>(TId id) where TId : notnull;
    Task<Result<IEnumerable<T>>> ListAsync();
    Task<Result> AddAsync(T entity);
    Task<Result> UpdateAsync(T entity);
    Task<Result> DeleteAsync(T entity);
    Task<Result<bool>> ExistsAsync<TId>(TId id) where TId : notnull;
}