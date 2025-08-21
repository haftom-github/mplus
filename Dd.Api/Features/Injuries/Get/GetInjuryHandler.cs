using Dd.Api.Features.Injuries.Repos;
using Dd.Api.Shared.Application.Cqrs;
using Dd.Api.Shared.Application.Repositories;
using Dd.Api.Shared.Application.Results;

namespace Dd.Api.Features.Injuries.Get;

public class GetInjuryHandler(IInjuryRepo repo, IUnitOfWork unit) : IQueryHandler<GetInjuryQuery, GetInjuryDto> {
    public async Task<Result<GetInjuryDto>> Handle(GetInjuryQuery request, CancellationToken cancellationToken) {
        try {
            var injury = await repo.GetByIdAsync(request.Id, cancellationToken);
            return injury switch {
                null => Result<GetInjuryDto>.Failure(ErrorType.NotFound),
                _ => Result<GetInjuryDto>.Success(injury.ToGetInjuryDto())
            };
        }
        catch (Exception ex) {
            return Result<GetInjuryDto>.Failure(ErrorType.Unexpected);
        }
    }
}