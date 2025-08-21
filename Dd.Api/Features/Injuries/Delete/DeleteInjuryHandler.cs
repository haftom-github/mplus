using Dd.Api.Features.Injuries.Repos;
using Dd.Api.Shared.Application.Cqrs;
using Dd.Api.Shared.Application.Repositories;
using Dd.Api.Shared.Application.Results;

namespace Dd.Api.Features.Injuries.Delete;

public class DeleteInjuryHandler(IInjuryRepo repo, IUnitOfWork unit) : ICommandHandler<DeleteInjuryCommand> {
    public async Task<Result> Handle(DeleteInjuryCommand request, CancellationToken cancellationToken) {
        try {
            var injury = await repo.GetByIdAsync(request.Id, cancellationToken);
            if (injury == null) {
                return Result.Failure(ErrorType.NotFound);
            }
            
            await repo.DeleteAsync(injury);
            await unit.CompleteAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Result.Failure(ErrorType.Unexpected);
        }
    }
}