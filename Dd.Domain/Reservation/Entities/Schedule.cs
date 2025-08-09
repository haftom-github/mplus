using Dd.Domain.Common.Entities;
using Dd.Domain.Reservation.Enums;
using Dd.Domain.Reservation.Utils;

namespace Dd.Domain.Reservation.Entities;

public class Schedule : Entity {
    private readonly List<DayOfWeek> _recurrenceDays = [];
    
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    
    public DateOnly StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }

    private DateOnly NormalizedStartDate => StartDate.AddDays(DayOfWeek.Sunday - StartDate.DayOfWeek);
    private DateOnly? NormalizedEndDate => EndDate?.AddDays(DayOfWeek.Sunday - EndDate.Value.DayOfWeek);

    private List<DayOfWeek> FirstWeekDifference =>
        Enumerable.Range(0, StartDate.DayNumber - NormalizedStartDate.DayNumber)
            .Select(i => StartDate.AddDays(i).DayOfWeek)
            .Where(d => _recurrenceDays.Contains(d))
            .ToList();
    private List<DayOfWeek> LastWeekDifference => EndDate == null || NormalizedEndDate == null ? [] :
        Enumerable.Range(0, EndDate.Value.DayNumber - NormalizedEndDate.Value.DayNumber)
            .Select(i => EndDate.Value.AddDays(-i).DayOfWeek)
            .Where(d => _recurrenceDays.Contains(d))
            .ToList();
    public RecurrenceType RecurrenceType { get; private set; } = RecurrenceType.Daily;
    
    // if recurring type is weekly
    public IReadOnlyList<DayOfWeek> RecurrenceDays => _recurrenceDays.AsReadOnly();
    public int RecurrenceInterval { get; private set; } = 1;

    public Schedule(TimeOnly startTime, TimeOnly endTime, DateOnly startDate, DateOnly? endDate = null) {
        
        if (startTime >= endTime)
            throw new ArgumentException("Start time must be earlier than end time.", nameof(startTime));
        
        if (endDate < startDate)
            throw new ArgumentException("End date cannot be earlier than start date.", nameof(endDate));
        
        if (startDate < DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentOutOfRangeException(nameof(startDate), "Start date cannot be in the past.");
        
        this.StartTime = startTime;
        this.EndTime = endTime;
        this.StartDate = startDate;
        this.EndDate = endDate;
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
    
    public void RecurDaily(int interval = 1) {
        if (interval <= 0)
            throw new ArgumentOutOfRangeException(nameof(interval), "Recurrence interval must be a positive integer.");
        
        RecurrenceType = RecurrenceType.Daily;
        RecurrenceInterval = interval;
    }
    
    public void UpdateRecurrenceInterval(int interval) {
        if (interval <= 0)
            throw new ArgumentOutOfRangeException(nameof(interval), "Recurrence interval must be a positive integer.");
        
        RecurrenceInterval = interval;
    }

    public bool Overlaps(Schedule other) {
        if (other == null)
            throw new ArgumentNullException(nameof(other), "Other schedule cannot be null.");
        
        if (StartTime >= other.EndTime || EndTime <= other.StartTime)
            return false;
        
        if (StartDate > other.EndDate || EndDate < other.StartDate)
            return false;

        switch (RecurrenceType) {
            case RecurrenceType.Daily:
                if (this.RecurrenceInterval == 1 || other.RecurrenceInterval == 1)
                    return true;
                
                var s1 = EndDate == null 
                    ? new Sequence(StartDate.DayNumber, RecurrenceInterval)
                    : new FiniteSequence(StartDate.DayNumber, EndDate.Value.DayNumber, RecurrenceInterval);
                var s2 = other.EndDate == null 
                    ? new Sequence(other.StartDate.DayNumber, other.RecurrenceInterval)
                    : new FiniteSequence(other.StartDate.DayNumber, other.EndDate.Value.DayNumber, other.RecurrenceInterval);
                
                return ScheduleMath.Overlaps(s1, s2);
            
            case RecurrenceType.Weekly:
                var commonDaysOfWeek = RecurrenceDays.Intersect(other.RecurrenceDays);
                if (!commonDaysOfWeek.Any())
                    return false;
                
                if (this.RecurrenceInterval == 1 || other.RecurrenceInterval == 1)
                    return true;
                
                s1 = EndDate == null 
                    ? new Sequence(NormalizedStartDate.DayNumber, RecurrenceInterval * 7)
                    : new FiniteSequence(NormalizedStartDate.DayNumber, EndDate.Value.DayNumber, RecurrenceInterval * 7);
                
                s2 = other.EndDate == null 
                    ? new Sequence(other.NormalizedStartDate.DayNumber, other.RecurrenceInterval * 7)
                    : new FiniteSequence(other.NormalizedStartDate.DayNumber, other.EndDate.Value.DayNumber, other.RecurrenceInterval * 7);
                
                return ScheduleMath.Overlaps(s1, s2);
                
            default:
                throw new NotImplementedException($"Recurrence type {RecurrenceType} is not implemented.");
        }
    }
}