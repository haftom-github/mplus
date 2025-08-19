using Dd.Api.Features.Schedules.Contracts;
using Dd.Api.Features.Schedules.Domain.Entities;
using Dd.Api.Shared.Application.Repositories;

namespace Dd.Api.Features.Schedules.Infrastructure;

public class InMemorySchedulesRepo
    : InMemoryRepo<Schedule, Guid>, IScheduleRepo {

    public InMemorySchedulesRepo() : base(s => s.Id) {
        var _2OClock = new TimeOnly(5, 0);
        var _11OClock = new TimeOnly(14, 0);
        var lastYear = new DateOnly(2024, 1, 1);
        var schedule1 = new Schedule(_2OClock, _11OClock, lastYear);
        var schedule2 = new Schedule(_2OClock.AddHours(8), _11OClock.AddHours(8), lastYear);
        Store.Add(schedule1.Id, schedule1);
        Store.Add(schedule2.Id, schedule2);
    }
}