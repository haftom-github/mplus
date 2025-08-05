using Dd.Domain.Common.Entities;
using Dd.Domain.Reservation.Enums;
using Dd.Domain.Reservation.Utils;

namespace Dd.Domain.Reservation.Entities;

public class Schedule : Entity
{
    private readonly List<DayOfWeek> _recurrenceDays = [];
    
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    
    public DateOnly? StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }
    public RecurrenceType RecurrenceType { get; private set; } = RecurrenceType.Daily;
    
    // if recurring type is weekly
    public IReadOnlyList<DayOfWeek> RecurrenceDays => _recurrenceDays.AsReadOnly();
    public int RecurrenceInterval { get; private set; } = 1;

    public Schedule(TimeOnly startTime, TimeOnly endTime, DateOnly? startDate, DateOnly? endDate = null) {
        
        if (startTime >= endTime)
            throw new ArgumentException("Start time must be earlier than end time.", nameof(startTime));
        
        if (endDate > startDate)
            throw new ArgumentException("End date cannot be earlier than start date.", nameof(endDate));
        
        if (startDate < DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentOutOfRangeException(nameof(startDate), "Start date cannot be in the past.");
        
        this.StartTime = startTime;
        this.EndTime = endTime;
        this.StartDate = startDate;
        this.EndDate = endDate;
    }
    
    public void RecurWeekly(List<DayOfWeek> daysOfWeek, int interval = 1)
    {
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

    private bool IsToLeft => StartDate == null && EndDate != null;
    private bool IsToRight => StartDate != null && EndDate == null;
    private bool IsToBoth => StartDate == null && EndDate == null;
    private bool IsToNone => StartDate != null && EndDate != null;
    
    public bool Overlaps(Schedule other) {
        if (other == null)
            throw new ArgumentNullException(nameof(other), "Other schedule cannot be null.");
        if (StartTime >= other.EndTime || EndTime <= other.StartTime)
            return false;
        
        if (StartDate > other.EndDate || EndDate < other.StartDate)
            return false;
        
        if ((this.IsToLeft 
             && (other.IsToRight || other.IsToNone)
             ) || this.IsToNone && other.IsToRight)
            if (this.EndDate < other.StartDate)
                return false;
        
        if ((this.IsToRight 
             && (other.IsToLeft || other.IsToNone)
             ) || this.IsToNone && other.IsToLeft)
            if (this.StartDate > other.EndDate)
                return false;

        switch (RecurrenceType) {
            case RecurrenceType.Daily:
                if (RecurrenceInterval <= 1 || other.RecurrenceInterval <= 1)
                    return true;
                if (StartDate == null || other.StartDate == null)
                    return true;
                var daysDifference = StartDate?.DayNumber - other.StartDate?.DayNumber ?? 0;
                return daysDifference % ScheduleMath.Gcd(RecurrenceInterval, other.RecurrenceInterval) == 0;
            
            case RecurrenceType.Weekly:
                if (!RecurrenceDays.Intersect(other.RecurrenceDays).Any())
                    return false;
                if (RecurrenceInterval <= 1 || other.RecurrenceInterval <= 1)
                    return true;
                if (StartDate == null || other.StartDate == null)
                    return true;
                
                daysDifference = StartDate?.DayNumber - other.StartDate?.DayNumber ?? 0;
                var weeksDifference = daysDifference / 7;
                return weeksDifference % ScheduleMath.Gcd(RecurrenceInterval, other.RecurrenceInterval) == 0;
            
            default:
                throw new NotImplementedException($"Recurrence type {RecurrenceType} is not implemented.");
        }
    }
}