using Dd.Domain.Common.Entities;
using Dd.Domain.Interfaces;
using Dd.Domain.Reservation.Enums;
using Dd.Domain.Reservation.Overlap;

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
        StartDate = FirstDayOnSchedule();
        EndDate = LastDayOnSchedule();
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
        var overlapDetector = OverlapDetectorFactory.Create(RecurrenceType);
        return overlapDetector.IsOverlapping(this, other);
    }

    private DateOnly FirstDayOnSchedule() {
        var date = StartDate.AddDays(0);
        while (!_recurrenceDays.Contains(date.DayOfWeek)) {
            date = date.AddDays(1);
        }
        return date;
    }

    private DateOnly? LastDayOnSchedule() {
        if (EndDate == null) return null;
        var interval = RecurrenceInterval * 7;
        var date = StartDate
            .AddDays((EndDate.Value.DayNumber - StartDate.DayNumber) / interval * interval);

        while (date.DayOfWeek != IDateTime.FirstDayOfWeek)
            date = date.AddDays(1);

        while (!_recurrenceDays.Contains(date.DayOfWeek))
            date = date.AddDays(-1);
        
        return date;
    }
}