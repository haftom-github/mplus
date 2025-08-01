using Dd.Domain.Common.Entities;

namespace Dd.Domain.Reservation.Entities;

public class TimeSlot
{
    public Physician? Physician { get; private set; }
    public Guid PhysicianId { get; private set; }
    
    public DateOnly Date { get; private set; }
    public int SlotNumber { get; private set; }
    public int Ticks { get; private set; }

    public TimeSlot(Physician physician, DateOnly date, int slotNumber, int ticks) {
        ArgumentNullException.ThrowIfNull(physician, nameof(physician));
        if(slotNumber < 0)
            throw new ArgumentOutOfRangeException(nameof(slotNumber), "Slot number must be a non negative integer.");
        
        if(ticks <= 0)
            throw new ArgumentOutOfRangeException(nameof(ticks), "Ticks must be a positive integer");
        
        if(date < DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentOutOfRangeException(nameof(date), "Date cannot be in the past.");
        
        this.Physician = physician;
        this.PhysicianId = physician.Id;
        this.Date = date;
        this.SlotNumber = slotNumber;
        this.Ticks = ticks;
    }
}