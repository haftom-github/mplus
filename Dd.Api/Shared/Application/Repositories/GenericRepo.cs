using Microsoft.EntityFrameworkCore;

namespace Dd.Api.Shared.Application.Repositories;

public class GenericRepo<T> : IRepo<T> where T : class {
    private readonly DbSet<T> _dbSet;

    protected GenericRepo(DbContext context) {
        var context1 = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context1.Set<T>();
    }

    public async Task<T?> GetByIdAsync<TId>(TId id) where TId : notnull {
        var result = await _dbSet.FindAsync(id);
        return result;
    }

    public async Task<IReadOnlyList<T>> ListAsync() {
            var result = await _dbSet.AsNoTracking().ToListAsync();
            return result;
    }

    public async Task AddAsync(T entity) {
        await _dbSet.AddAsync(entity);
    }

    public Task UpdateAsync(T entity) {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity) {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync<TId>(TId id) where TId : notnull {
        var entity = await _dbSet.FindAsync(id);
        return entity is not null;
    }
}