using Dd.Api.Features.Schedules.Contracts;
using Dd.Api.Features.Schedules.Domain.Entities;
using Dd.Api.Shared.Application.Cqrs;
using Dd.Api.Shared.Application.Results;

namespace Dd.Api.Features.Schedules.Get;

public class GetSchedulesHandler(IScheduleRepo repo) 
    : IQueryHandler<GetSchedulesQuery, IEnumerable<Schedule>> {
    public async Task<Result<IEnumerable<Schedule>>> 
        Handle(GetSchedulesQuery request, CancellationToken cancellationToken) {
        
            var schedules = await repo.ListAsync(cancellationToken);
            return Result<IEnumerable<Schedule>>.Success(schedules);
    }
}