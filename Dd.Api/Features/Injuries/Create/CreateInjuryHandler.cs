using Dd.Api.Features.Injuries.Repos;
using Dd.Api.Shared.Application.Cqrs;
using Dd.Api.Shared.Application.Repositories;
using Dd.Api.Shared.Application.Results;
using Dd.Api.Shared.Domain.MasterData;

namespace Dd.Api.Features.Injuries.Create;

public class CreateInjuryHandler(IInjuryRepo repo, IUnitOfWork unit) : ICommandHandler<CreateInjuryCommand, Guid> {
    public async Task<Result<Guid>> Handle(CreateInjuryCommand request, CancellationToken cancellationToken) {
        try {
            var newInjury = new Injury(request.Name, request.Description);
            var newInjuryId = newInjury.Id;
            await repo.AddAsync(newInjury, cancellationToken);
            await unit.CompleteAsync(cancellationToken);
            return Result<Guid>.Success(newInjuryId);
        }
        catch (Exception) {
            return Result<Guid>.Failure(ErrorType.Unexpected);
        }
    }
}