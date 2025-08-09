namespace Dd.Domain.Reservation.Entities;

public class TimeSlot
{
    public Guid PhysicianId { get; private set; }
    
    public TimeSpan Gap { get; private set; } = TimeSpan.Zero;
    public int SlotNumber { get; private set; }
    public int Ticks { get; private set; }

    public TimeSlot(Guid physicianId, int slotNumber, int ticks) {
        if(slotNumber < 0)
            throw new ArgumentOutOfRangeException(nameof(slotNumber), "Slot number must be a non negative integer.");
        
        if(ticks <= 0)
            throw new ArgumentOutOfRangeException(nameof(ticks), "Ticks must be a positive integer");
        
        this.PhysicianId = physicianId;
        this.SlotNumber = slotNumber;
        this.Ticks = ticks;
    }
    
    public TimeSlot(Guid physicianId, int slotNumber, int ticks, TimeSpan gap) : this(physicianId, slotNumber, ticks) {
        if(gap < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(gap), "Gap cannot be negative.");
        
        this.Gap = gap;
    }
}