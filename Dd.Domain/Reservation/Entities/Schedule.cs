using Dd.Domain.Common.Entities;
using Dd.Domain.Reservation.Enums;
using Dd.Domain.Reservation.Utils;

namespace Dd.Domain.Reservation.Entities;

public class Schedule : Entity {
    private readonly HashSet<DayOfWeek> _recurrenceDays = [];
    
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    
    public DateOnly StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }
    
    public RecurrenceType RecurrenceType { get; private set; } = RecurrenceType.Daily;
    
    // if recurring type is weekly
    public IReadOnlySet<DayOfWeek> RecurrenceDays => _recurrenceDays;
    public int RecurrenceInterval { get; private set; } = 1;
    // end of properties
    
    
    // start of rules
    private DateOnly NormalizedStartDate => Normalize(StartDate, DayOfWeek.Sunday) ?? StartDate;

    private DateOnly? NormalizedEndDate => Normalize(EndDate, DayOfWeek.Sunday);
    private ISet<DayOfWeek> FirstWeekDifference() {
        var firstWeekDifference = new HashSet<DayOfWeek>();
        var date = NormalizedStartDate.AddDays(0);
        while (date < StartDate) {
            if(_recurrenceDays.Contains(date.DayOfWeek))
                firstWeekDifference.Add(date.DayOfWeek);
            date = date.AddDays(1);
        }
        return firstWeekDifference;
    }

    private ISet<DayOfWeek> LastWeekDifference() {
        if (EndDate == null) return new HashSet<DayOfWeek>();
        HashSet<DayOfWeek> lastWeekDaysOnSchedule = [];
        var date = NormalizedStartDate.AddDays(0);
        while (date <= EndDate) {
            if(_recurrenceDays.Contains(date.DayOfWeek))
                lastWeekDaysOnSchedule.Add(date.DayOfWeek);
            date = date.AddDays(1);
        }
        return _recurrenceDays.Except(lastWeekDaysOnSchedule).ToHashSet();
    }
    
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

        var set = new HashSet<DayOfWeek>(daysOfWeek);
        _recurrenceDays.Clear();
        _recurrenceDays.UnionWith(set);
        
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
                
                s2 = other.EndDate == null || other.NormalizedEndDate == null
                    ? new Sequence(other.NormalizedStartDate.DayNumber, other.RecurrenceInterval * 7)
                    : new FiniteSequence(other.NormalizedStartDate.DayNumber, other.NormalizedEndDate.Value.DayNumber, other.RecurrenceInterval * 7);
                
                var overlaps = ScheduleMath.FirstOverlap(s1, s2);
                if (overlaps == null) return false;
                if (overlaps?.count is null or > 2) return true;
                if (NormalizedStartDate.DayNumber != other.NormalizedStartDate.DayNumber)
                    return true;
                if (overlaps?.f != NormalizedStartDate.DayNumber)
                    return true;
                if (EndDate != null) {
                    
                }
                
                
            default:
                throw new NotImplementedException($"Recurrence type {RecurrenceType} is not implemented.");
        }
    }

    private static DateOnly? Normalize(DateOnly? date, DayOfWeek startOfWeek) {
        if (date == null) return null;
        var normalized = date.Value.AddDays(0);
        while (normalized.DayOfWeek != startOfWeek)
            normalized = date.Value.AddDays(-1);

        return normalized;
    }
}