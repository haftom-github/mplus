using Dd.Domain.Common.Entities;
using Dd.Domain.Reservation.Enums;

namespace Dd.Domain.Reservation.Entities;

public class Appointment : Entity
{
    public Patient? Patient { get; set; }
    public Guid PatientId { get; set; }
    
    public TimeSlot? TimeSlot { get; set; }
    public Guid TimeSlotId { get; set; }

    public string? Reason { get; set; }
    
    public AppointmentType? AppointmentType { get; set; }
    public Guid AppointmentTypeId { get; set; }
    
    public AppointmentStatus Status { get; private set; } = AppointmentStatus.Scheduled;
    public TimeSlot? InitialTimeSlot { get; set; }
    public Guid? InitialTimeSlotId { get; set; }
    
    public int PostponeCount { get; private set; } = 0;
    
    public DateTime? CheckedInAt { get; private set; }
    public DateTime? ExaminationStartedAt { get; private set; }
    public DateTime? ExaminationEndedAt { get; private set; }
    
    public void CheckIn()
    {
        if (Status != AppointmentStatus.Scheduled)
            throw new InvalidOperationException("Appointment already checked in or not scheduled.");
        
        Status = AppointmentStatus.CheckedIn;
        CheckedInAt = DateTime.UtcNow;
    }

    public void StartExamination()
    {
        if (Status != AppointmentStatus.CheckedIn)
            throw new InvalidOperationException("Cannot start examination for an appointment that is not checked in.");
        
        Status = AppointmentStatus.InProgress;
        ExaminationStartedAt = DateTime.UtcNow;
    }
    
    public void EndExamination()
    {
        if (Status != AppointmentStatus.InProgress)
            throw new InvalidOperationException("Cannot end examination for an appointment that is not in progress.");
        
        Status = AppointmentStatus.Completed;
        ExaminationEndedAt = DateTime.UtcNow;
    }
    
    public void PostponeToSelf(TimeSlot newTimeSlot)
    {
        if (newTimeSlot == null)
            throw new ArgumentNullException(nameof(newTimeSlot), "New time slot cannot be null.");
        
        if (newTimeSlot.PhysicianId != TimeSlot?.PhysicianId)
            throw new InvalidOperationException("Cannot postpone to a time slot with a different physician.");
        
        if (newTimeSlot.Index <= TimeSlot.Index)
            throw new InvalidOperationException("Cannot postpone to a time slot that is earlier than or equal to the current time slot.");
        
        if (Status != AppointmentStatus.Scheduled
            && Status != AppointmentStatus.CheckedIn
            && Status != AppointmentStatus.Postponed)
            throw new InvalidOperationException("Cannot postpone an appointment that is under examination.");

        if (Status != AppointmentStatus.Postponed) {
            InitialTimeSlot = TimeSlot;
            InitialTimeSlotId = TimeSlotId;
        }
        
        TimeSlot = newTimeSlot;
        TimeSlotId = newTimeSlot.Id;
        
        Status = AppointmentStatus.Postponed;
        PostponeCount++;
    }
}