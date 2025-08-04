using Dd.Domain.Common.Entities;

namespace Dd.Domain.Reservation.Entities;

public class TimeSlot
{
    public Physician? Physician { get; private set; }
    public Guid PhysicianId { get; private set; }
    
    public TimeSpan Gap { get; private set; } = TimeSpan.Zero;
    public int SlotNumber { get; private set; }
    public int Ticks { get; private set; }

    public TimeSlot(Physician physician, int slotNumber, int ticks) {
        ArgumentNullException.ThrowIfNull(physician, nameof(physician));
        if(slotNumber < 0)
            throw new ArgumentOutOfRangeException(nameof(slotNumber), "Slot number must be a non negative integer.");
        
        if(ticks <= 0)
            throw new ArgumentOutOfRangeException(nameof(ticks), "Ticks must be a positive integer");
        
        this.Physician = physician;
        this.PhysicianId = physician.Id;
        this.SlotNumber = slotNumber;
        this.Ticks = ticks;
    }
    
    public TimeSlot(Physician physician, int slotNumber, int ticks, TimeSpan gap) : this(physician, slotNumber, ticks)
    {
        if(gap < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(gap), "Gap cannot be negative.");
        
        this.Gap = gap;
    }
}