namespace Dd.Api.Features.Schedules.Domain.Entities;

public class WorkSchedule(TimeOnly startTime, TimeOnly endTime, DateOnly startDate, DateOnly? endDate = null)
    : Schedule(startTime, endTime, startDate, endDate);