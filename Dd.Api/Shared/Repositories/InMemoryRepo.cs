using Dd.Api.Shared.Results;

namespace Dd.Api.Shared.Repositories;

public class InMemoryRepo<T, TId>(Func<T, TId> idSelector) : IRepo<T>
    where T : class where TId : notnull {
    
    protected readonly Dictionary<TId, T> Store = new();
    private readonly Func<T, TId> _idSelector 
        = idSelector ?? throw new ArgumentNullException(nameof(idSelector));

    public Task<T?> GetByIdAsync<TIdKey>(TIdKey id) where TIdKey : notnull {
        if (id is TId typedId && Store.TryGetValue(typedId, out var entity)) 
            return Task.FromResult<T?>(entity);
    
        return Task.FromResult(default(T?));
    }

    public Task<IReadOnlyList<T>> ListAsync() {
        return Task.FromResult<IReadOnlyList<T>>(Store.Values.ToList());
    }

    public Task AddAsync(T entity) {
        var id = _idSelector(entity);
        Store[id] = entity;
        return Task.FromResult(Result.Success());
    }

    public Task UpdateAsync(T entity) {
        var id = _idSelector(entity);
        if (Store.ContainsKey(id)) 
            Store[id] = entity;
    
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity) {
        var id = _idSelector(entity);
        Store.Remove(id);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync<TIdKey>(TIdKey id) where TIdKey : notnull {
        return Task.FromResult(id is TId typedId && Store.ContainsKey(typedId));
    }
}