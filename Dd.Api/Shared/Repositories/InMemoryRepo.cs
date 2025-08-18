using Dd.Api.Shared.Results;

namespace Dd.Api.Shared.Repositories;

public class InMemoryRepo<T, TId>(Func<T, TId> idSelector) : IRepo<T>
    where T : class where TId : notnull {
    
    protected readonly Dictionary<TId, T> Store = new();
    private readonly Func<T, TId> _idSelector 
        = idSelector ?? throw new ArgumentNullException(nameof(idSelector));

    public Task<Result<T>> GetByIdAsync<TIdKey>(TIdKey id) where TIdKey : notnull {
        try {
            if (id is TId typedId && Store.TryGetValue(typedId, out var entity)) 
                return Task.FromResult(Result<T>.Success(entity));
        
            return Task.FromResult(Result<T>.Failure(ErrorType.NotFound));
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Task.FromResult(Result<T>.Failure(ErrorType.Unknown));
        }
    }

    public Task<Result<IEnumerable<T>>> ListAsync() {
        try {
            return Task.FromResult(Result<IEnumerable<T>>.Success(Store.Values.ToList()));
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Task.FromResult(Result<IEnumerable<T>>.Failure(ErrorType.Unknown));
        }
    }

    public Task<Result> AddAsync(T entity) {
        try {
            var id = _idSelector(entity);
            Store[id] = entity;
            return Task.FromResult(Result.Success());
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Task.FromResult(Result.Failure(ErrorType.Unknown));
        }
    }

    public Task<Result> UpdateAsync(T entity) {
        try {
            var id = _idSelector(entity);
            if (Store.ContainsKey(id)) 
                Store[id] = entity;
        
            return Task.FromResult(Result.Success());
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Task.FromResult(Result.Failure(ErrorType.Unknown));
        }
    }

    public Task<Result> DeleteAsync(T entity) {
        try {
            var id = _idSelector(entity);
            Store.Remove(id);
            return Task.FromResult(Result.Success());
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Task.FromResult(Result.Failure(ErrorType.Unknown));
        }
    }

    public Task<Result<bool>> ExistsAsync<TIdKey>(TIdKey id) where TIdKey : notnull {
        try {
            return Task.FromResult(Result<bool>.Success(id is TId typedId && Store.ContainsKey(typedId)));
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Task.FromResult(Result<bool>.Failure(ErrorType.Unknown));
        }
    }
}