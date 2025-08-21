using Dd.Api.Features.Injuries.Repos;
using Dd.Api.Shared.Application.Cqrs;
using Dd.Api.Shared.Application.GlobalResults;
using Dd.Api.Shared.Application.Repositories;

namespace Dd.Api.Features.Injuries.Get;

public class GetAllInjuriesHandler(IInjuryRepo repo, IUnitOfWork unit) 
    : IQueryHandler<GetAllInjuriesQuery, IEnumerable<GetInjuryDto>> {
    public async Task<Result<IEnumerable<GetInjuryDto>>> Handle(GetAllInjuriesQuery request, CancellationToken cancellationToken) {
        try {
            var injuries = await repo.ListAsync(cancellationToken);
            return Result<IEnumerable<GetInjuryDto>>
                .Success(injuries.Select(i => i.ToGetInjuryDto()));
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Result<IEnumerable<GetInjuryDto>>.Failure(ErrorType.Unexpected);
        }
    }
}