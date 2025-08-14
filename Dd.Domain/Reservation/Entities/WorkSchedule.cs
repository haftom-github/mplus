namespace Dd.Domain.Reservation.Entities;

public class WorkSchedule(TimeOnly startTime, TimeOnly endTime, DateOnly startDate, DateOnly? endDate = null)
    : Schedule(startTime, endTime, startDate, endDate);