using Dd.Domain.Common.Entities;

namespace Dd.Domain.Reservation.Entities;

public class AppointmentType : Entity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public TimeSpan Duration { get; set; }
}