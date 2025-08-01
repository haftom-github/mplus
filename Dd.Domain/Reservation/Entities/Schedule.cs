using Dd.Domain.Common.Entities;

namespace Dd.Domain.Reservation.Entities;

public enum RecurrenceType
{
    Daily,
    Weekly,
}

public class Schedule : Entity
{
    public Physician? Physician { get; set; }
    public Guid PhysicianId { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    
    public bool IsRecurring { get; set; }
    
    public RecurrenceType RecurrenceType { get; set; }
    
    // if recurring type is weekly
    public List<DayOfWeek>? RecurrenceDays { get; set; } = [];
    public int? RecurrenceInterval { get; set; }
}