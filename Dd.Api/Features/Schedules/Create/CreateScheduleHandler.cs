using Dd.Api.Features.Schedules.Contracts;
using Dd.Api.Features.Schedules.Domain.Entities;
using MediatR;

namespace Dd.Api.Features.Schedules.Create;

public class CreateScheduleHandler(IScheduleRepo repo) : IRequestHandler<CreateScheduleCommand, Guid> {
    public async Task<Guid> Handle(CreateScheduleCommand request, CancellationToken cancellationToken) {
        var newSchedule = new Schedule(request.StartTime, request.EndTime, request.StartDate, request.EndDate);
        // switch (request.RecurrenceType) {
        //     case RecurrenceType.Daily:
        //         newSchedule.UpdateRecurrenceInterval(request.RecurrenceInterval);
        //         break;
        //     case RecurrenceType.Weekly:
        //         newSchedule.RecurWeekly(request.DaysOfWeek, request.RecurrenceInterval);
        //         break;
        // }
        await repo.AddAsync(newSchedule);
        return newSchedule.Id;
    }
}