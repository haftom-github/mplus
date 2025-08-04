using Dd.Domain.Common.Entities;
using Dd.Domain.Reservation.Enums;

namespace Dd.Domain.Reservation.Entities;

public class Schedule : Entity
{
    private readonly List<DayOfWeek> _recurrenceDays = [];
    public Physician? Physician { get; private set; }
    public Guid PhysicianId { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    
    public DateOnly StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }
    public RecurrenceType RecurrenceType { get; private set; } = RecurrenceType.Daily;
    
    // if recurring type is weekly
    public IReadOnlyList<DayOfWeek>? RecurrenceDays => _recurrenceDays?.AsReadOnly();
    public int RecurrenceInterval { get; private set; } = 1;

    public Schedule(Physician physician, TimeOnly startTime, TimeOnly endTime, DateOnly startDate, DateOnly? endDate = null) {
        ArgumentNullException.ThrowIfNull(physician, nameof(physician));
        
        if (startTime >= endTime)
            throw new ArgumentException("Start time must be earlier than end time.", nameof(startTime));
        
        if (startDate < DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentOutOfRangeException(nameof(startDate), "Start date cannot be in the past.");
        
        this.Physician = physician;
        this.PhysicianId = physician.Id;
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
}