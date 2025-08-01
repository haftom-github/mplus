using Dd.Domain.Common.Entities;

namespace Dd.Domain.Reservation.Entities;

public class TimeSlot : Entity
{
    public Physician? Physician { get; set; }
    public Guid PhysicianId { get; set; }
    
    public DateOnly Date { get; set; }
    public int Index { get; set; }
    public int Ticks { get; set; }
}