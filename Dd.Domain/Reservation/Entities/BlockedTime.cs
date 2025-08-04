using Dd.Domain.Common.Entities;
using Dd.Domain.Reservation.Enums;

namespace Dd.Domain.Reservation.Entities;

public class BlockedTime : Entity {
    private readonly List<DayOfWeek> _recurrenceDays = [];
    public bool BlocksAllPhysicians { get; private set; }
    
    public TimeOnly? StartTime { get; private set; }
    public TimeOnly? EndTime { get; private set; }

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
        
        this.StartTime = startTime;
        this.EndTime = endTime;
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
        _recurrenceDays.AddRange(daysOfWeek);
        
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

    public bool IsBlocked(DateOnly date) {
        if (StartDate > date || EndDate < date) return false;
        if (StartTime != null && EndTime != null) return false;

        return RecurrenceType switch {
            RecurrenceType.Daily => true,
            RecurrenceType.Weekly when RecurrenceDays.Count == 0 => false,
            RecurrenceType.Weekly => RecurrenceDays.Contains(date.DayOfWeek),
            _ => throw new NotImplementedException($"Recurrence type {RecurrenceType} is not implemented.")
        };
    }
    
    public bool IsBlocked(DateTime time) {
        if (StartDate > DateOnly.FromDateTime(time) || EndDate < DateOnly.FromDateTime(time))
            return false;

        switch (RecurrenceType) {
            case RecurrenceType.Daily: 
                if (StartTime == null || EndTime == null) return true;
                return TimeOnly.FromDateTime(time) >= StartTime && TimeOnly.FromDateTime(time) <= EndTime;

            case RecurrenceType.Weekly:
                if (RecurrenceDays.Count == 0) return false;
                if (!RecurrenceDays.Contains(time.DayOfWeek)) return false;
                if (StartTime == null || EndTime == null) return true;
                return TimeOnly.FromDateTime(time) >= StartTime && TimeOnly.FromDateTime(time) <= EndTime;

            default:
                throw new NotImplementedException($"Recurrence type {RecurrenceType} is not implemented.");
        }
    }
}