namespace Dd.Domain.Reservation.Entities;

public class TimeSlot
{
    public Guid Id { get; set; }
    public Guid PhysicianId { get; private set; }
    public DateOnly Date { get; }
    public TimeOnly StartTime { get; }
    public TimeSpan Span { get; private set; }

    public TimeSlot(Guid physicianId, DateOnly date, TimeOnly startTime, TimeSpan span) {
        if( span <= TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(span), "time span can not be zero");
        
        this.PhysicianId = physicianId;
        this.Date = date;
        this.StartTime = startTime;
        this.Span = span;
    }

    public TimeSlot(Guid physicianId, DateTime dateTime, TimeSpan span) : this(physicianId,
        DateOnly.FromDateTime(dateTime), TimeOnly.FromDateTime(dateTime), span) {}
}