using Dd.Domain.Common.Entities;
using Dd.Domain.Reservation.Enums;

namespace Dd.Domain.Reservation.Entities;

public class BlockedTime : Entity {
    private readonly HashSet<DayOfWeek> _recurrenceDays = [];
    public bool BlocksAllPhysicians { get; private set; }
    
    public TimeOnly StartTime { get; }
    public TimeOnly EndTime { get; }

    public DateOnly StartDate { get; }
    public DateOnly? EndDate { get; }
    
    public RecurrenceType RecurrenceType { get; private set; } = RecurrenceType.Daily;
    
    // if recurring type is weekly
    public IReadOnlySet<DayOfWeek> RecurrenceDays => _recurrenceDays;
    public int RecurrenceInterval { get; private set; } = 1;
    
    public string? Reason { get; set; }
    public BlockedTimeType BlockedTimeType { get; private set; }

    public BlockedTime(
        BlockedTimeType blockedTimeType, 
        DateOnly startDate, TimeOnly? startTime, 
        TimeOnly? endTime, DateOnly? endDate = null) {
        
        startTime ??= TimeOnly.MinValue;
        endTime ??= TimeOnly.MaxValue;
        
        if (startTime == endTime)
            throw new ArgumentException("end time can not be equal to start time", nameof(endTime));

        if (startDate > endDate)
            throw new ArgumentException("Start date cannot be later than end date.", nameof(startDate));
        
        this.StartTime = startTime!.Value;
        this.EndTime = endTime!.Value;
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
        _recurrenceDays.UnionWith(daysOfWeek.ToHashSet());
        
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

        switch (RecurrenceType) {
            case RecurrenceType.Daily:
                if (RecurrenceInterval <= 1) return true;
                var days = date.DayNumber - StartDate.DayNumber;
                return (days % RecurrenceInterval) == 0;

            case RecurrenceType.Weekly:
                if (RecurrenceDays.Count == 0) return false;
                if (!RecurrenceDays.Contains(date.DayOfWeek)) return false;
                if (RecurrenceInterval <= 1) return true;
                var daysSinceStart = date.DayNumber - StartDate.DayNumber;
                var weeksSinceStart = daysSinceStart / 7;
                return (weeksSinceStart % RecurrenceInterval) == 0;

            default:
                throw new NotImplementedException($"Recurrence type {RecurrenceType} is not implemented.");
        }
    }
    
    public bool IsBlocked(DateOnly date, TimeOnly startTime, TimeOnly endTime) {
        if (startTime >= endTime)
            throw new ArgumentException("Start time must be earlier than end time.", nameof(startTime));
        if (StartDate > date || EndDate < date) return false;
        if (startTime >= EndTime || endTime <= StartTime) return false;

        switch (RecurrenceType) {
            case RecurrenceType.Daily:
                if (RecurrenceInterval <= 1) return true;
                var days = date.DayNumber - StartDate.DayNumber;
                return (days % RecurrenceInterval) == 0;

            case RecurrenceType.Weekly:
                if (RecurrenceDays.Count == 0) return false;
                if (!RecurrenceDays.Contains(date.DayOfWeek)) return false;
                if (RecurrenceInterval <= 1) return true;
                var daysSinceStart = date.DayNumber - StartDate.DayNumber;
                var weeksSinceStart = daysSinceStart / 7;
                return (weeksSinceStart % RecurrenceInterval) == 0;

            default:
                throw new NotImplementedException($"Recurrence type {RecurrenceType} is not implemented.");
        }
        
    }
}