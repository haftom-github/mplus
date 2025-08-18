using Dd.Api.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Dd.Api.Shared.Repositories;

public class GenericRepo<T> : IRepo<T> where T : class {
    private readonly DbSet<T> _dbSet;

    protected GenericRepo(DbContext context) {
        var context1 = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context1.Set<T>();
    }

    public async Task<Result<T>> GetByIdAsync<TId>(TId id) where TId : notnull {
        var result = await _dbSet.FindAsync(id);
        return result switch {
            null => Result<T>.Failure(ErrorType.NotFound),
            _ => Result<T>.Success(result)
        };
    }

    public async Task<Result<IEnumerable<T>>> ListAsync() {
        try {
            var result = await _dbSet.AsNoTracking().ToListAsync();
            return Result<IEnumerable<T>>.Success(result);
        }
        catch (Exception e) {
            return Result<IEnumerable<T>>.Failure(ErrorType.Unknown);
        }
    }

    public async Task<Result> AddAsync(T entity) {
        try {
            await _dbSet.AddAsync(entity);
            return Result.Success();
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Result.Failure(ErrorType.Unknown);
        }
    }

    public Task<Result> UpdateAsync(T entity) {
        try {
            _dbSet.Update(entity);
            return Task.FromResult(Result.Success());
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Task.FromResult(Result.Failure(ErrorType.Unknown));
        }
    }

    public Task<Result> DeleteAsync(T entity) {
        try {
            _dbSet.Remove(entity);
            return Task.FromResult(Result.Success());
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Task.FromResult(Result.Failure(ErrorType.Unknown));
        }
    }

    public async Task<Result<bool>> ExistsAsync<TId>(TId id) where TId : notnull {
        try {
            var entity = await _dbSet.FindAsync(id);
            return Result<bool>.Success(entity is not null);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Result<bool>.Failure(ErrorType.Unknown);
        }
    }
}