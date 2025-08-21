using Dd.Api.Shared.Infrastructure.Persistence;

namespace Dd.Api.Shared.Application.Repositories;

public class UnitOfWork(AppDbContext context) 
    : IUnitOfWork {

    public async Task<int> CompleteAsync(CancellationToken cancellationToken = default) {
        return await context.SaveChangesAsync(cancellationToken);
    }
    
    public void Dispose() {
        context.Dispose();
    }
}