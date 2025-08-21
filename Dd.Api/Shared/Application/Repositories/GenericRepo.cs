using Microsoft.EntityFrameworkCore;

namespace Dd.Api.Shared.Application.Repositories;

public class GenericRepo<T> : IRepo<T> where T : class {
    private readonly DbSet<T> _dbSet;

    protected GenericRepo(DbContext context, CancellationToken cancellationToken = default) {
        var context1 = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context1.Set<T>();
    }

    public async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull {
        var result = await _dbSet.FindAsync([id], cancellationToken: cancellationToken);
        return result;
    }

    public async Task<IReadOnlyList<T>> ListAsync(CancellationToken cancellationToken = default) {
            var result = await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
            return result;
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default) {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(T entity) {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity) {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull {
        var entity = await _dbSet.FindAsync([id], cancellationToken: cancellationToken);
        return entity is not null;
    }
}