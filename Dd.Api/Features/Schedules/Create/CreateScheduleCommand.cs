using Dd.Api.Features.Schedules.Domain.Enums;
using MediatR;

namespace Dd.Api.Features.Schedules.Create;

public class CreateScheduleCommand : IRequest<Guid> {
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    
    public int RecurrenceInterval { get; set; }
    public RecurrenceType RecurrenceType { get; set; }
    public List<DayOfWeek> DaysOfWeek { get; set; } = [];
}