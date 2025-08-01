using Dd.Domain.Reservation.Entities;
using Dd.Domain.Reservation.Enums;

namespace Dd.Domain.Test.Reservation.Entities;

public class AppointmentTests {
    // CheckIn method tests
    [Fact]
    public void CheckIn_ShouldSetStatusToCheckedIn_WhenCalled() {
        // Arrange
        var appointment = new Appointment { };

        // Act
        appointment.CheckIn();

        // Assert
        Assert.Equal(AppointmentStatus.CheckedIn, appointment.Status);
    }

    [Fact]
    public void CheckIn_ShouldSetCheckedInAt_WhenCalled() {
        // Arrange
        var appointment = new Appointment { };

        // Act
        appointment.CheckIn();

        // Assert
        Assert.NotNull(appointment.CheckedInAt);
        Assert.True((DateTime.UtcNow - appointment.CheckedInAt) < TimeSpan.FromMinutes(1));
    }

    [Fact]
    public void CheckIn_ShouldThrowInvalidOperationException_WhenStatusNotScheduled() {
        // Arrange
        var appointment = new Appointment { };
        appointment.CheckIn();

        Assert.Throws<InvalidOperationException>(() => appointment.CheckIn());
    }

    // startExamination method tests
    [Fact]
    public void StartExamination_ShouldSetStatusToInProgress_WhenCalled() {
        // Arrange
        var appointment = new Appointment();
        appointment.CheckIn(); // Ensure the appointment is checked in

        // Act
        appointment.StartExamination();

        // Assert
        Assert.Equal(AppointmentStatus.InProgress, appointment.Status);
    }

    [Fact]
    public void StartExamination_ShouldSetExaminationStartedAt_WhenCalled() {
        // Arrange
        var appointment = new Appointment();
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
        var appointment = new Appointment();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => appointment.StartExamination());
        Assert.Equal(AppointmentStatus.Scheduled, appointment.Status);
    }

    // EndExamination method tests
    [Fact]
    public void EndExamination_ShouldSetStatusToCompleted_WhenCalled() {
        var appointment = new Appointment();
        appointment.CheckIn();
        appointment.StartExamination();

        appointment.EndExamination();
        Assert.Equal(AppointmentStatus.Completed, appointment.Status);
    }

    [Fact]
    public void EndExamination_ShouldSetExaminationEndedAt_WhenCalled() {
        var appointment = new Appointment();
        appointment.CheckIn();
        appointment.StartExamination();

        appointment.EndExamination();

        Assert.NotNull(appointment.ExaminationEndedAt);
        Assert.True((DateTime.UtcNow - appointment.ExaminationEndedAt) < TimeSpan.FromMinutes(1));
    }

    [Fact]
    public void EndExamination_ShouldThrowInvalidOperationException_WhenStatusNotInProgress() {
        var appointment = new Appointment();
        Assert.Throws<InvalidOperationException>(() => appointment.EndExamination());
        Assert.Equal(AppointmentStatus.Scheduled, appointment.Status);

        appointment.CheckIn();
        Assert.Throws<InvalidOperationException>(() => appointment.EndExamination());
        Assert.Equal(AppointmentStatus.CheckedIn, appointment.Status);
    }

    // Postpone method tests

    [Fact]
    public void PostponeToSelf_ShouldThrowInvalidOperationException_WhenNewTimeSlotIsNotOfTheSamePhysician() {
        var appointment = new Appointment { };
        var initialTimeSlot = new TimeSlot {
            Date = DateOnly.FromDateTime(DateTime.UtcNow), Index = 0, Ticks = 0,
            PhysicianId = Guid.NewGuid()
        };

        appointment.TimeSlot = initialTimeSlot;
        appointment.TimeSlotId = initialTimeSlot.Id;
        
        var newTimeSlot = new TimeSlot {
            Date = DateOnly.FromDateTime(DateTime.UtcNow), Index = 1, Ticks = 0,
            PhysicianId = Guid.NewGuid() // Different physician
        };
        
        Assert.Throws<InvalidOperationException>(() => appointment.PostponeToSelf(newTimeSlot));
        Assert.Equal(AppointmentStatus.Scheduled, appointment.Status);
        Assert.Equal(0, appointment.PostponeCount);
        Assert.Equal(initialTimeSlot, appointment.TimeSlot);
        Assert.Equal(initialTimeSlot.Id, appointment.TimeSlotId);
    }
    
    [Fact]
    public void PostponeToSelf_ShouldSetStatusToPostponed_WhenCalled() {
        var appointment = new Appointment { };
        var newTimeSlot = new TimeSlot { Date = DateOnly.FromDateTime(DateTime.UtcNow), Index = 1, Ticks = 0 };
        appointment.PostponeToSelf(newTimeSlot);
        
        Assert.Equal(AppointmentStatus.Postponed, appointment.Status);
    }

    [Fact]
    public void PostponeToSelf_ShouldIncrementPostponeCount_WhenCalled() {
        var appointment = new Appointment { };
        var newTimeSlot = new TimeSlot { Date = DateOnly.FromDateTime(DateTime.UtcNow), Index = 1, Ticks = 0 };
        
        appointment.PostponeToSelf(newTimeSlot);
        
        Assert.Equal(1, appointment.PostponeCount);
    }

    [Fact]
    public void PostponeToSelf_ShouldSetNewTimeSlot_WhenCalled() {
        var appointment = new Appointment { };
        var newTimeSlot = new TimeSlot { Date = DateOnly.FromDateTime(DateTime.UtcNow), Index = 1, Ticks = 0 };
        
        appointment.PostponeToSelf(newTimeSlot);
        
        Assert.Equal(newTimeSlot, appointment.TimeSlot);
        Assert.Equal(newTimeSlot.Id, appointment.TimeSlotId);
    }

    [Fact]
    public void PostponeToSelf_ShouldSetInitialTimeSlot_WhenNotAlreadyPostponed() {
        var appointment = new Appointment { };
        var initialTimeSlot = new TimeSlot { Date = DateOnly.FromDateTime(DateTime.UtcNow), Index = 0, Ticks = 0 };
        appointment.TimeSlot = initialTimeSlot;
        appointment.TimeSlotId = initialTimeSlot.Id;
        
        var newTimeSlot = new TimeSlot { Date = DateOnly.FromDateTime(DateTime.UtcNow), Index = 1, Ticks = 0 };
        
        appointment.PostponeToSelf(newTimeSlot);
        
        Assert.Equal(initialTimeSlot, appointment.InitialTimeSlot);
        Assert.Equal(initialTimeSlot.Id, appointment.InitialTimeSlotId);
    }
    
    [Fact]
    public void PostponeToSelf_ShouldNotSetInitialTimeSlot_WhenAlreadyPostponed() {
        var appointment = new Appointment { };
        var initialTimeSlot = new TimeSlot { Date = DateOnly.FromDateTime(DateTime.UtcNow), Index = 0, Ticks = 0 };
        appointment.TimeSlot = initialTimeSlot;
        appointment.TimeSlotId = initialTimeSlot.Id;
        
        var newTimeSlot1 = new TimeSlot { Date = DateOnly.FromDateTime(DateTime.UtcNow), Index = 1, Ticks = 0 };
        appointment.PostponeToSelf(newTimeSlot1);
        var newTimeSlot2 = new TimeSlot { Date = DateOnly.FromDateTime(DateTime.UtcNow), Index = 2, Ticks = 0 };
        appointment.PostponeToSelf(newTimeSlot2);
        Assert.Equal(initialTimeSlot, appointment.InitialTimeSlot);
        Assert.Equal(initialTimeSlot.Id, appointment.InitialTimeSlotId);
    }

    [Fact]
    public void PostponeToSelf_ShouldThrowInvalidOperationException_WhenStatusNotScheduledOrCheckedInOrPostponed() {
        var appointment = new Appointment { };
        appointment.CheckIn(); // Change status to CheckedIn
        appointment.StartExamination(); // Change status to InProgress
        var newTimeSlot = new TimeSlot { Date = DateOnly.FromDateTime(DateTime.UtcNow), Index = 1, Ticks = 0 };
        
        Assert.Throws<InvalidOperationException>(() => appointment.PostponeToSelf(newTimeSlot));
        Assert.Equal(AppointmentStatus.InProgress, appointment.Status);
        
        // ensure the same happens when the appointment is completed
        appointment.EndExamination(); // Change status to Completed
        Assert.Throws<InvalidOperationException>(() => appointment.PostponeToSelf(newTimeSlot));
        Assert.Equal(AppointmentStatus.Completed, appointment.Status);
    }

    [Fact]
    public void PostponeToSelf_ShouldThrowInvalidOperationException_WhenPostponedToTheSameTimeSlot() {
        var appointment = new Appointment { };
        appointment.CheckIn();
        var initialTimeSlot = new TimeSlot { Date = DateOnly.FromDateTime(DateTime.UtcNow), Index = 0, Ticks = 0 };
        appointment.TimeSlot = initialTimeSlot;
        appointment.TimeSlotId = initialTimeSlot.Id;
        
        Assert.Throws<InvalidOperationException>(() => appointment.PostponeToSelf(initialTimeSlot));
        Assert.Equal(AppointmentStatus.CheckedIn, appointment.Status);
        Assert.Equal(0, appointment.PostponeCount);
        Assert.Equal(initialTimeSlot, appointment.TimeSlot);
        Assert.Equal(initialTimeSlot.Id, appointment.TimeSlotId);
    }

    [Fact]
    public void PostponeToSelf_ShouldThrowInvalidOperationException_WhenPostponedToPreviousTimeSlot() {
        var appointment = new Appointment { };
        var initialTimeSlot = new TimeSlot { Index = 0, Ticks = 0, PhysicianId = Guid.NewGuid()};
        appointment.TimeSlot = initialTimeSlot;
        appointment.TimeSlotId = initialTimeSlot.Id;
        
        var newTimeSlot = new TimeSlot { Index = 1, Ticks = 0, PhysicianId = initialTimeSlot.PhysicianId };
        appointment.PostponeToSelf(newTimeSlot);

        Assert.Throws<InvalidOperationException>(() => appointment.PostponeToSelf(initialTimeSlot));
        Assert.Equal(AppointmentStatus.Postponed, appointment.Status);
        Assert.Equal(1, appointment.PostponeCount);
        Assert.Equal(newTimeSlot, appointment.TimeSlot);
        Assert.Equal(newTimeSlot.Id, appointment.TimeSlotId);
        Assert.Equal(initialTimeSlot, appointment.InitialTimeSlot);
        Assert.Equal(initialTimeSlot.Id, appointment.InitialTimeSlotId);
    }
}