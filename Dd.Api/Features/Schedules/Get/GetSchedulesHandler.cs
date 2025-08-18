using Dd.Api.Features.Schedules.Contracts;
using Dd.Api.Features.Schedules.Domain.Entities;
using MediatR;

namespace Dd.Api.Features.Schedules.Get;

public class GetSchedulesHandler(IScheduleRepo repo) : IRequestHandler<GetSchedulesQuery, IEnumerable<Schedule>> {
    public async Task<IEnumerable<Schedule>> Handle(GetSchedulesQuery request, CancellationToken cancellationToken) {
        var schedules = await repo.ListAsync();
        return schedules;
    }
}