using Dd.Api.Features.Injuries.Repos;
using Dd.Api.Shared.Application.Cqrs;
using Dd.Api.Shared.Application.GlobalResults;
using Dd.Api.Shared.Application.Repositories;

namespace Dd.Api.Features.Injuries.Update;

public class UpdateInjuryHandler(IInjuryRepo repo, IUnitOfWork unit) : ICommandHandler<UpdateInjuryCommand> {
    public async Task<Result> Handle(UpdateInjuryCommand request, CancellationToken cancellationToken) {
        try {
            Console.WriteLine("Reached UpdateInjuryHandler");
            var injury = await repo.GetByIdAsync(request.Id, cancellationToken);
            if (injury == null) {
                return Result.Failure(ErrorType.NotFound);
            }
            await repo.UpdateAsync(injury);
            injury.Name = request.Name ?? injury.Name;
            injury.Description = request.Description ?? injury.Description;
            await unit.CompleteAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Result.Failure(ErrorType.Unexpected);
        }
    }
}