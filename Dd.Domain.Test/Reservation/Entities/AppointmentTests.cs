using Dd.Domain.Common.Entities;
using Dd.Domain.Reservation.Entities;
using Dd.Domain.Reservation.Enums;

namespace Dd.Domain.Test.Reservation.Entities;

public class AppointmentTests {

    private readonly Patient _patient1;
    private readonly Physician _physician1;
    private readonly Physician _physician2;
    
    private readonly TimeSlot _timeSlot10;
    private readonly TimeSlot _timeSlot11;
    private readonly TimeSlot _timeSlot12;
    
    private readonly TimeSlot _timeSlot20;
    private readonly TimeSlot _timeSlot21;
    private readonly TimeSlot _timeSlot22;
    
    private readonly DateOnly _tomorrow = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1));
    private const int FirstSlotNumber = 0;
    private const int SecondSlotNumber = 1;
    private const int ThirdSlotNumber = 2;
    private const int ThreeTicks = 3;
    public AppointmentTests() {
        _patient1 = new Patient();
        
        _physician1 = new Physician();
        _timeSlot10 = new TimeSlot(_physician1.Id, FirstSlotNumber, ThreeTicks);
        _timeSlot11 = new TimeSlot(_physician1.Id, SecondSlotNumber, ThreeTicks);
        _timeSlot12 = new TimeSlot(_physician1.Id, ThirdSlotNumber, ThreeTicks);
        
        _physician2 = new Physician();
        _timeSlot20 = new TimeSlot(_physician2.Id, FirstSlotNumber, ThreeTicks);
        _timeSlot21 = new TimeSlot(_physician2.Id, SecondSlotNumber, ThreeTicks);
        _timeSlot22 = new TimeSlot(_physician2.Id, ThirdSlotNumber, ThreeTicks);
    }
    
    // Constructor tests
    [Fact]
    public void Constructor_ShouldInitializeProperties_WhenCalledWithValidParameters() {
        
        // Act
        var appointment = new Appointment(_patient1.Id, _timeSlot10);
        
        // Assert
        Assert.NotNull(appointment);
        Assert.Equal(_patient1.Id, appointment.PatientId);
        Assert.Equal(_timeSlot10.PhysicianId, appointment.PhysicianId);
        Assert.Equal(_timeSlot10.SlotNumber, appointment.SlotNumber);
        Assert.Equal(AppointmentStatus.Scheduled, appointment.Status);
        Assert.Equal(_timeSlot10.SlotNumber, appointment.SlotNumber);
        Assert.Equal(_timeSlot10.PhysicianId, appointment.PhysicianId);
        Assert.Null(appointment.InitialPhysicianId);
        Assert.Null(appointment.InitialSlotNumber);
        Assert.Equal(0, appointment.PostponeCount);
        Assert.Null(appointment.CheckedInAt);
        Assert.Null(appointment.ExaminationStartedAt);
        Assert.Null(appointment.ExaminationEndedAt);
    }
    
    // CheckIn method tests
    [Fact]
    public void CheckIn_ShouldSetStatusToCheckedIn_WhenCalled() {
        // Arrange
        var appointment = new Appointment(_patient1.Id, _timeSlot10);

        // Act
        appointment.CheckIn();

        // Assert
        Assert.Equal(AppointmentStatus.CheckedIn, appointment.Status);
    }

    [Fact]
    public void CheckIn_ShouldSetCheckedInAt_WhenCalled() {
        // Arrange
        var appointment = new Appointment(_patient1.Id, _timeSlot10);

        // Act
        appointment.CheckIn();

        // Assert
        Assert.NotNull(appointment.CheckedInAt);
        Assert.True((DateTime.UtcNow - appointment.CheckedInAt) < TimeSpan.FromMinutes(1));
    }

    [Fact]
    public void CheckIn_ShouldThrowInvalidOperationException_WhenStatusNotScheduled() {
        // Arrange
        var appointment = new Appointment(_patient1.Id, _timeSlot10);
        appointment.CheckIn();

        Assert.Throws<InvalidOperationException>(() => appointment.CheckIn());
    }

    // startExamination method tests
    [Fact]
    public void StartExamination_ShouldSetStatusToInProgress_WhenCalled() {
        // Arrange
        var appointment = new Appointment(_patient1.Id, _timeSlot10);
        appointment.CheckIn(); // Ensure the appointment is checked in

        // Act
        appointment.StartExamination();

        // Assert
        Assert.Equal(AppointmentStatus.InProgress, appointment.Status);
    }

    [Fact]
    public void StartExamination_ShouldSetExaminationStartedAt_WhenCalled() {
        // Arrange
        var appointment = new Appointment(_patient1.Id, _timeSlot10);
        appointment.CheckIn(); // Ensure the appointment is checked in

        // Act
        appointment.StartExamination();

        // Assert
        Assert.NotNull(appointment.ExaminationStartedAt);
        Assert.True((DateTime.UtcNow - appointment.ExaminationStartedAt) < TimeSpan.FromMinutes(1));
    }

    [Fact]
    public void StartExamination_ShouldThrowInvalidOperationException_WhenStatusNotCheckedIn() {
        // Arrange
        var appointment = new Appointment(_patient1.Id, _timeSlot10);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => appointment.StartExamination());
        Assert.Equal(AppointmentStatus.Scheduled, appointment.Status);
    }

    // EndExamination method tests
    [Fact]
    public void EndExamination_ShouldSetStatusToCompleted_WhenCalled() {
        var appointment = new Appointment(_patient1.Id, _timeSlot10);
        appointment.CheckIn();
        appointment.StartExamination();

        appointment.EndExamination();
        Assert.Equal(AppointmentStatus.Completed, appointment.Status);
    }

    [Fact]
    public void EndExamination_ShouldSetExaminationEndedAt_WhenCalled() {
        var appointment = new Appointment(_patient1.Id, _timeSlot10);
        appointment.CheckIn();
        appointment.StartExamination();

        appointment.EndExamination();

        Assert.NotNull(appointment.ExaminationEndedAt);
        Assert.True((DateTime.UtcNow - appointment.ExaminationEndedAt) < TimeSpan.FromMinutes(1));
    }

    [Fact]
    public void EndExamination_ShouldThrowInvalidOperationException_WhenStatusNotInProgress() {
        var appointment = new Appointment(_patient1.Id, _timeSlot10);
        Assert.Throws<InvalidOperationException>(() => appointment.EndExamination());
        Assert.Equal(AppointmentStatus.Scheduled, appointment.Status);

        appointment.CheckIn();
        Assert.Throws<InvalidOperationException>(() => appointment.EndExamination());
        Assert.Equal(AppointmentStatus.CheckedIn, appointment.Status);
    }

    // Postpone method tests

    [Fact]
    public void PostponeToSelf_ShouldThrowInvalidOperationException_WhenNewTimeSlotIsNotOfTheSamePhysician() {
        var appointment = new Appointment(_patient1.Id, _timeSlot10);
        
        Assert.Throws<InvalidOperationException>(() => appointment.PostponeToSelf(_timeSlot21));
        Assert.Equal(AppointmentStatus.Scheduled, appointment.Status);
        Assert.Equal(0, appointment.PostponeCount);
        Assert.Equal(_timeSlot10.SlotNumber, appointment.SlotNumber);
        Assert.Equal(_timeSlot10.PhysicianId, appointment.PhysicianId);
    }
    
    [Fact]
    public void PostponeToSelf_ShouldSetStatusToPostponed_WhenCalled() {
        var appointment = new Appointment(_patient1.Id, _timeSlot10);
        appointment.PostponeToSelf(_timeSlot11);
        
        Assert.Equal(AppointmentStatus.Postponed, appointment.Status);
    }

    [Fact]
    public void PostponeToSelf_ShouldIncrementPostponeCount_WhenCalled() {
        var appointment = new Appointment(_patient1.Id, _timeSlot10);
        
        appointment.PostponeToSelf(_timeSlot11);
        
        Assert.Equal(1, appointment.PostponeCount);
    }

    [Fact]
    public void PostponeToSelf_ShouldSetNewTimeSlot_WhenCalled() {
        var appointment = new Appointment(_patient1.Id, _timeSlot10);
        
        appointment.PostponeToSelf(_timeSlot11);
        
        Assert.Equal(_timeSlot11.SlotNumber, appointment.SlotNumber);
        Assert.Equal(_timeSlot11.PhysicianId, appointment.PhysicianId);
    }

    [Fact]
    public void PostponeToSelf_ShouldSetInitialTimeSlot_WhenNotAlreadyPostponed() {
        var appointment = new Appointment(_patient1.Id, _timeSlot10);
        
        appointment.PostponeToSelf(_timeSlot11);
        
        Assert.Equal(_timeSlot10.SlotNumber, appointment.InitialSlotNumber);
        Assert.Equal(_timeSlot10.PhysicianId, appointment.InitialPhysicianId);
    }
    
    [Fact]
    public void PostponeToSelf_ShouldNotSetInitialTimeSlot_WhenAlreadyPostponed() {
        var appointment = new Appointment(_patient1.Id, _timeSlot10);
        
        appointment.PostponeToSelf(_timeSlot11);
        appointment.PostponeToSelf(_timeSlot12);
        Assert.Equal(_timeSlot10.SlotNumber, appointment.InitialSlotNumber);
        Assert.Equal(_timeSlot10.PhysicianId, appointment.InitialPhysicianId);
    }

    [Fact]
    public void PostponeToSelf_ShouldThrowInvalidOperationException_WhenStatusNotScheduledOrCheckedInOrPostponed() {
        var appointment = new Appointment(_patient1.Id, _timeSlot10);
        appointment.CheckIn(); // Change status to CheckedIn
        appointment.StartExamination(); // Change status to InProgress
        
        Assert.Throws<InvalidOperationException>(() => appointment.PostponeToSelf(_timeSlot11));
        Assert.Equal(AppointmentStatus.InProgress, appointment.Status);
        
        // ensure the same happens when the appointment is completed
        appointment.EndExamination(); // Change status to Completed
        Assert.Throws<InvalidOperationException>(() => appointment.PostponeToSelf(_timeSlot12));
        Assert.Equal(AppointmentStatus.Completed, appointment.Status);
    }

    [Fact]
    public void PostponeToSelf_ShouldThrowInvalidOperationException_WhenPostponedToTheSameTimeSlot() {
        var appointment = new Appointment(_patient1.Id, _timeSlot10);
        appointment.CheckIn();
        
        Assert.Throws<InvalidOperationException>(() => appointment.PostponeToSelf(_timeSlot10));
        Assert.Equal(AppointmentStatus.CheckedIn, appointment.Status);
        Assert.Equal(0, appointment.PostponeCount);
        Assert.Equal(_timeSlot10.SlotNumber, appointment.SlotNumber);
        Assert.Equal(_timeSlot10.PhysicianId, appointment.PhysicianId);
    }

    [Fact]
    public void PostponeToSelf_ShouldThrowInvalidOperationException_WhenPostponedToPreviousTimeSlot() {
        var appointment = new Appointment(_patient1.Id, _timeSlot10);
        
        appointment.PostponeToSelf(_timeSlot11);

        Assert.Throws<InvalidOperationException>(() => appointment.PostponeToSelf(_timeSlot10));
        Assert.Equal(AppointmentStatus.Postponed, appointment.Status);
        Assert.Equal(1, appointment.PostponeCount);
        Assert.Equal(_timeSlot11.SlotNumber, appointment.SlotNumber);
        Assert.Equal(_timeSlot11.PhysicianId, appointment.PhysicianId);
        Assert.Equal(_timeSlot10.SlotNumber, appointment.InitialSlotNumber);
        Assert.Equal(_timeSlot10.PhysicianId, appointment.InitialPhysicianId);
    }
    
    // Cancel method tests
    [Fact]
    public void Cancel_ShouldSetStatusToCancelled_WhenCalled() {
        var appointment = new Appointment(_patient1.Id, _timeSlot10);
        appointment.Cancel();
        Assert.Equal(AppointmentStatus.Cancelled, appointment.Status);
        
        appointment = new Appointment(_patient1.Id, _timeSlot10);
        appointment.CheckIn(); // Change status to CheckedIn
        appointment.Cancel();
        Assert.Equal(AppointmentStatus.Cancelled, appointment.Status);
        
        appointment = new Appointment(_patient1.Id, _timeSlot10);
        appointment.PostponeToSelf(_timeSlot11); // Change status to Postponed
        appointment.Cancel();
        Assert.Equal(AppointmentStatus.Cancelled, appointment.Status);
    }

    [Fact]
    public void Cancel_ShouldThrowInvalidOperationException_WhenAppointmentIsAlreadyHandled() {
        var appointment = new Appointment(_patient1.Id, _timeSlot10);
        appointment.CheckIn();
        appointment.StartExamination();
        Assert.Throws<InvalidOperationException>(() => appointment.Cancel());
        Assert.Equal(AppointmentStatus.InProgress, appointment.Status);
        
        appointment.EndExamination();
        Assert.Throws<InvalidOperationException>(() => appointment.Cancel());
        Assert.Equal(AppointmentStatus.Completed, appointment.Status);
    }
}