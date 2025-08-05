using Dd.Domain.Common.Entities;
using Dd.Domain.Reservation.Enums;

namespace Dd.Domain.Reservation.Entities;

public class BlockedTime : Entity {
    private readonly List<DayOfWeek> _recurrenceDays = [];
    public bool BlocksAllPhysicians { get; private set; }
    
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }

    public DateOnly? StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }
    
    public RecurrenceType RecurrenceType { get; private set; } = RecurrenceType.Daily;
    
    // if recurring type is weekly
    public IReadOnlyList<DayOfWeek> RecurrenceDays => _recurrenceDays.AsReadOnly();
    public int RecurrenceInterval { get; private set; } = 1;
    
    public string? Reason { get; set; }
    public BlockedTimeType BlockedTimeType { get; private set; }

    public BlockedTime(
        BlockedTimeType blockedTimeType, 
        TimeOnly? startTime = null, TimeOnly? endTime = null,
        DateOnly? startDate = null, 
        DateOnly? endDate = null) {
        
        if (startTime >= endTime)
            throw new ArgumentException("Start time must be earlier than end time.", nameof(startTime));

        if (startDate.HasValue && endDate.HasValue)
            if (startDate > endDate)
                throw new ArgumentException("Start date cannot be later than end date.", nameof(startDate));
        
        this.StartTime = startTime ?? TimeOnly.MinValue;
        this.EndTime = endTime ?? TimeOnly.MaxValue;
        this.StartDate = startDate;
        this.EndDate = endDate;
        this.BlockedTimeType = blockedTimeType;
    }
    
    public void BlocksAll() {
        this.BlocksAllPhysicians = true;
    }
    public void RecurWeekly(List<DayOfWeek> daysOfWeek, int interval = 1) {
        if (daysOfWeek == null || daysOfWeek.Count == 0)
            throw new ArgumentException("Days of week cannot be null or empty.", nameof(daysOfWeek));
        
        if (interval <= 0)
            throw new ArgumentOutOfRangeException(nameof(interval), "Recurrence interval must be a positive integer.");
        
        _recurrenceDays.Clear();
        _recurrenceDays.AddRange(daysOfWeek.Distinct());
        
        RecurrenceType = RecurrenceType.Weekly;
        RecurrenceInterval = interval;
    }
    
    public void RecurDaily(int interval = 1)
    {
        if (interval <= 0)
            throw new ArgumentOutOfRangeException(nameof(interval), "Recurrence interval must be a positive integer.");
        
        RecurrenceType = RecurrenceType.Daily;
        RecurrenceInterval = interval;
    }
    
    public void UpdateRecurrenceInterval(int interval)
    {
        if (interval <= 0)
            throw new ArgumentOutOfRangeException(nameof(interval), "Recurrence interval must be a positive integer.");
        
        RecurrenceInterval = interval;
    }

    public bool IsWholeDayBlocked(DateOnly date) {
        if (StartDate > date || EndDate < date) return false;
        if (StartTime != TimeOnly.MinValue && EndTime != TimeOnly.MaxValue) return false;

        return RecurrenceType switch {
            RecurrenceType.Daily => true,
            RecurrenceType.Weekly when RecurrenceDays.Count == 0 => false,
            RecurrenceType.Weekly => RecurrenceDays.Contains(date.DayOfWeek),
            _ => throw new NotImplementedException($"Recurrence type {RecurrenceType} is not implemented.")
        };
    }
    
    public bool IsBlocked(DateOnly date, TimeOnly startTime, TimeOnly endTime) {
        if (startTime >= endTime)
            throw new ArgumentException("Start time must be earlier than end time.", nameof(startTime));
        if (StartDate > date || EndDate < date) return false;
        if (startTime >= EndTime || endTime <= StartTime) return false;

        switch (RecurrenceType) {
            case RecurrenceType.Daily:
                if (StartDate == null || RecurrenceInterval <= 1) return true;
                var days = date.DayNumber - StartDate?.DayNumber ?? 1;
                return (days % RecurrenceInterval) == 0;

            case RecurrenceType.Weekly:
                if (RecurrenceDays.Count == 0) return false;
                if (!RecurrenceDays.Contains(date.DayOfWeek)) return false;
                if (StartDate == null || RecurrenceInterval <= 1) return true;
                var daysSinceStart = date.DayNumber - StartDate?.DayNumber ?? 1;
                var weeksSinceStart = daysSinceStart / 7;
                return (weeksSinceStart % RecurrenceInterval) == 0;

            default:
                throw new NotImplementedException($"Recurrence type {RecurrenceType} is not implemented.");
        }
        
    }
}