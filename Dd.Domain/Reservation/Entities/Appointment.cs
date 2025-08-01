using Dd.Domain.Common.Entities;
using Dd.Domain.Reservation.Enums;

namespace Dd.Domain.Reservation.Entities;

public class Appointment : Entity
{
    public Patient? Patient { get; private set; }
    public Guid PatientId { get; private set; }
    
    public TimeSlot? TimeSlot { get; private set; }
    public Guid PhysicianId { get; private set; }
    public int SlotNumber { get; private set; }

    public string? Reason { get; set; }
    
    public AppointmentType? AppointmentType { get; private set; }
    public Guid AppointmentTypeId { get; private set; }
    
    public AppointmentStatus Status { get; private set; } = AppointmentStatus.Scheduled;
    public TimeSlot? InitialTimeSlot { get; private set; }
    public Guid? InitialPhysicianId { get; private set; }
    public int? InitialSlotNumber { get; private set; }
    
    public int PostponeCount { get; private set; } = 0;
    
    public DateTime? CheckedInAt { get; private set; }
    public DateTime? ExaminationStartedAt { get; private set; }
    public DateTime? ExaminationEndedAt { get; private set; }

    public Appointment(Patient patient, TimeSlot timeSlot) {
        ArgumentNullException.ThrowIfNull(patient, nameof(patient));
        ArgumentNullException.ThrowIfNull(timeSlot, nameof(timeSlot));
        
        this.Patient = patient;
        this.PatientId = patient.Id;
        this.TimeSlot = timeSlot;
        this.PhysicianId = timeSlot.PhysicianId;
        this.SlotNumber = timeSlot.SlotNumber;
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
        
        if (newTimeSlot.PhysicianId != TimeSlot?.PhysicianId)
            throw new InvalidOperationException("Cannot postpone to a time slot with a different physician.");
        
        if (newTimeSlot.SlotNumber <= TimeSlot.SlotNumber)
            throw new InvalidOperationException("Cannot postpone to a time slot that is earlier than or equal to the current time slot.");
        
        if (Status != AppointmentStatus.Scheduled
            && Status != AppointmentStatus.CheckedIn
            && Status != AppointmentStatus.Postponed)
            throw new InvalidOperationException("Cannot postpone an appointment that is under examination.");

        if (Status != AppointmentStatus.Postponed) {
            InitialTimeSlot = TimeSlot;
            InitialPhysicianId = TimeSlot.PhysicianId;
            InitialSlotNumber = TimeSlot.SlotNumber;
        }
        
        TimeSlot = newTimeSlot;
        SlotNumber = newTimeSlot.SlotNumber;
        PhysicianId = newTimeSlot.PhysicianId;
        
        Status = AppointmentStatus.Postponed;
        PostponeCount++;
    }
}