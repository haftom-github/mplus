using Dd.Domain.Common.Entities;

namespace Dd.Domain.Reservation.Entities;

public class AppointmentType : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Ticks { get; private set; }

    public AppointmentType(string name, string description, int ticks) {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        ArgumentNullException.ThrowIfNull(description, nameof(description));
        
        if (ticks <= 0)
            throw new ArgumentOutOfRangeException(nameof(ticks), "Ticks must be a positive integer.");
        
        this.Name = name;
        this.Description = description;
        this.Ticks = ticks;
    }
}