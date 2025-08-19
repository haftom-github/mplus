using Dd.Api.Features.Schedules.Contracts;
using Dd.Api.Features.Schedules.Domain.Entities;
using Dd.Api.Features.Schedules.Domain.Enums;
using Dd.Api.Shared.Application.Cqrs;
using Dd.Api.Shared.Application.Results;

namespace Dd.Api.Features.Schedules.Create;

public class CreateScheduleHandler(IScheduleRepo repo) : ICommandHandler<CreateScheduleCommand, Guid> {
    public async Task<Result<Guid>> Handle(CreateScheduleCommand request, CancellationToken cancellationToken) {
        try {
            var newSchedule = new Schedule(request.StartTime, request.EndTime, request.StartDate, request.EndDate);
            switch (request.RecurrenceType) {
                case RecurrenceType.Daily:
                    newSchedule.UpdateRecurrenceInterval(request.RecurrenceInterval);
                    break;
                case RecurrenceType.Weekly:
                    newSchedule.RecurWeekly(request.DaysOfWeek, request.RecurrenceInterval);
                    break;
            }
            await repo.AddAsync(newSchedule);
            return Result<Guid>.Success(newSchedule.Id);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Result<Guid>.Failure(ErrorType.Unknown, "Failed to create schedule");
        }
    }
}