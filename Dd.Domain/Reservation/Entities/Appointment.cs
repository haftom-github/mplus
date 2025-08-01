using Dd.Domain.Common.Entities;

namespace Dd.Domain.Reservation.Entities;

public enum AppointmentStatus
{
    Scheduled,
    CheckedIn,
    InProgress,
    Postponed,
    Rescheduled,
    Completed,
    Cancelled,
    NoShow
}

public class Appointment : Entity
{
    public Physician? Physician { get; set; }
    public Guid PhysicianId { get; set; }
    
    public Patient? Patient { get; set; }
    public Guid PatientId { get; set; }
    
    public TimeSlot? TimeSlot { get; set; }
    public Guid TimeSlotId { get; set; }

    public string? Reason { get; set; }
    
    public AppointmentType? AppointmentType { get; set; }
    public Guid AppointmentTypeId { get; set; }
    
    public AppointmentStatus Status { get; private set; } = AppointmentStatus.Scheduled;
    
    public DateTime? CheckedInAt { get; private set; }
    public DateTime? ExaminationStartedAt { get; private set; }
    public DateTime? ExaminationEndedAt { get; private set; }
}