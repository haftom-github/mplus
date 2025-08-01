using Dd.Domain.Common.Entities;
using Dd.Domain.Reservation.Enums;

namespace Dd.Domain.Reservation.Entities;

public class BlockedTime : Entity
{
    public Physician? Physician { get; set; }
    public Guid? PhysicianId { get; set; }

    public List<PhysicianGroup>? PhysicianGroup { get; set; } = [];
    public bool BlocksAllPhysicians { get; set; }
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    public bool IsRecurring { get; set; }
    public RecurrenceType RecurrenceType { get; set; }
    
    // if recurring type is weekly
    public List<DayOfWeek>? RecurrenceDays { get; set; } = [];
    public int? RecurrenceInterval { get; set; }
    
    public string? Reason { get; set; }
    public BlockedTimeType BlockedTimeType { get; set; }
}