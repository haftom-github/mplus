using Dd.Domain.Common.Entities;
using Dd.Domain.Reservation.Enums;

namespace Dd.Domain.Reservation.Entities;

public class Appointment : Entity
{
    public Guid PatientId { get; private set; }
    
    public Guid PhysicianId { get; private set; }
    
    public TimeSlot? Slot { get; private set; }
    public Guid SlotId { get; private set; }

    public string? Reason { get; set; }
    
    public AppointmentType? AppointmentType { get; private set; }
    public Guid AppointmentTypeId { get; private set; }
    
    public AppointmentStatus Status { get; private set; } = AppointmentStatus.Scheduled;
    public Guid InitialPhysicianId { get; private set; }
    public Guid InitialSlotId { get; private set; }
    
    public int PostponeCount { get; private set; }
    
    public DateTime? CheckedInAt { get; private set; }
    public DateTime? ExaminationStartedAt { get; private set; }
    public DateTime? ExaminationEndedAt { get; private set; }

    public Appointment(Guid patientId, TimeSlot timeSlot) {
        ArgumentNullException.ThrowIfNull(timeSlot, nameof(timeSlot));
        
        this.PatientId = patientId;
        this.PhysicianId = timeSlot.PhysicianId;
        this.SlotId = timeSlot.Id;
        this.Slot = timeSlot;
        this.InitialPhysicianId = PhysicianId;
        this.InitialSlotId = SlotId;
    }
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
        
        if (newTimeSlot.PhysicianId != PhysicianId)
            throw new InvalidOperationException("Cannot postpone to a time slot with a different physician.");
        
        if (newTimeSlot.Date <= Slot.Date && newTimeSlot.StartTime <= Slot.StartTime)
            throw new InvalidOperationException("Cannot postpone to a time slot that is earlier than or equal to the current time slot.");
        
        if (Status != AppointmentStatus.Scheduled
            && Status != AppointmentStatus.CheckedIn
            && Status != AppointmentStatus.Postponed)
            throw new InvalidOperationException("Cannot postpone an appointment that is under examination.");
        
        SlotId = newTimeSlot.Id;
        PhysicianId = newTimeSlot.PhysicianId;
        Slot = newTimeSlot;
        Status = AppointmentStatus.Postponed;
        PostponeCount++;
    }

    public void Cancel() {
        if (Status is not (AppointmentStatus.Scheduled or AppointmentStatus.Postponed or AppointmentStatus.CheckedIn) )
            throw new InvalidOperationException("Cannot cancel an appointment that is already handled");

        Status = AppointmentStatus.Cancelled;
    }
    
    public void NoShow() {
        if (Status is not (AppointmentStatus.Scheduled or AppointmentStatus.Postponed))
            throw new InvalidOperationException("Cannot mark a patient that has already checked in as no-show");
        
        Status = AppointmentStatus.NoShow;
    }
}