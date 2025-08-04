using System.Runtime.InteropServices.JavaScript;
using Dd.Domain.Common.Entities;
using Dd.Domain.Reservation.Entities;
using Dd.Domain.Reservation.Enums;

namespace Dd.Domain.Test.Reservation.Entities;

public class BlockedTimeTests {
    // constructor tests
    [Fact]
    public void Constructor_ShouldInitializeProperties_WhenValidParametersAreProvided() {
        // Arrange
        var startTime = new TimeOnly(9, 0);
        var endTime = new TimeOnly(17, 0);
        var startDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1));
        var endDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(2));
        const BlockedTimeType blockedTimeType = BlockedTimeType.Vacation;

        // Act
        var blockedTime = new BlockedTime(blockedTimeType, startTime, endTime);

        // Assert
        Assert.Null(blockedTime.Physician);
        Assert.Null(blockedTime.PhysicianId);
        Assert.Null(blockedTime.StartDate);
        Assert.Null(blockedTime.EndDate);
        Assert.Empty(blockedTime.PhysicianGroup);
        Assert.False(blockedTime.BlocksAllPhysicians);
        Assert.Equal(startTime, blockedTime.StartTime);
        Assert.Equal(endTime, blockedTime.EndTime);
        Assert.Equal(blockedTimeType, blockedTime.BlockedTimeType);
        
        blockedTime = new BlockedTime(blockedTimeType, startTime, endTime, startDate, endDate);
        
        Assert.Null(blockedTime.Physician);
        Assert.Null(blockedTime.PhysicianId);
        Assert.Empty(blockedTime.PhysicianGroup);
        Assert.False(blockedTime.BlocksAllPhysicians);
        Assert.Equal(startTime, blockedTime.StartTime);
        Assert.Equal(endTime, blockedTime.EndTime);
        Assert.Equal(startDate, blockedTime.StartDate);
        Assert.Equal(endDate, blockedTime.EndDate);
        Assert.Equal(blockedTimeType, blockedTime.BlockedTimeType);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentExceptionWhenEndTimeComesBeforeStartTime() {
        // Arrange
        var startTime = new TimeOnly(17, 0);
        var endTime = new TimeOnly(9, 0);
        const BlockedTimeType blockedTimeType = BlockedTimeType.Vacation;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new BlockedTime(blockedTimeType, startTime, endTime));
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentExceptionWhenEndTimeComesAfterEndTime() {
        // Arrange
        var startTime = new TimeOnly(9, 0);
        var endTime = new TimeOnly(17, 0);
        var startDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1));
        var endDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(0));
        const BlockedTimeType blockedTimeType = BlockedTimeType.Vacation;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new BlockedTime(blockedTimeType, startTime, endTime, startDate, endDate));
    }
    
    // AssignToPhysician tests
    [Fact]
    public void AssignToPhysician_ShouldSetPhysicianAndPhysicianId_WhenValidPhysicianIsProvided() {
        // Arrange
        var physician = new Physician();
        var blockedTime = new BlockedTime(BlockedTimeType.Vacation, new TimeOnly(9, 0), new TimeOnly(17, 0));

        // Act
        blockedTime.AssignToPhysician(physician);

        // Assert
        Assert.Equal(physician, blockedTime.Physician);
        Assert.Equal(physician.Id, blockedTime.PhysicianId);
    }

    [Fact]
    public void AssignToPhysician_ShouldThrowArgumentNullException_WhenPhysicianIsNull() {
        // Arrange
        var blockedTime = new BlockedTime(BlockedTimeType.Vacation, new TimeOnly(9, 0), new TimeOnly(17, 0));

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => blockedTime.AssignToPhysician(null!));
    }
    
    // AssignToPhysicianGroup tests
    [Fact]
    public void AssignToPhysicianGroup_ShouldAddPhysicianGroup_WhenValidPhysicianGroupIsProvided() {
        // Arrange
        var physicianGroup = new PhysicianGroup();
        var blockedTime = new BlockedTime(BlockedTimeType.Vacation, new TimeOnly(9, 0), new TimeOnly(17, 0));

        // Act
        blockedTime.AssignToPhysicianGroup(physicianGroup);

        // Assert
        Assert.Contains(physicianGroup, blockedTime.PhysicianGroup);
    }

    [Fact]
    public void AssignToPhysicianGroup_ShouldThrowArgumentNullException_WhenPhysicianGroupIsNull() {
        // Arrange
        var blockedTime = new BlockedTime(BlockedTimeType.Vacation, new TimeOnly(9, 0), new TimeOnly(17, 0));

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => blockedTime.AssignToPhysicianGroup(null!));
    }
    
    [Fact]
    public void AssignToPhysicianGroup_ShouldNotAddDuplicatePhysicianGroup_WhenAlreadyAssigned() {
        // Arrange
        var physicianGroup = new PhysicianGroup();
        var blockedTime = new BlockedTime(BlockedTimeType.Vacation, new TimeOnly(9, 0), new TimeOnly(17, 0));
        blockedTime.AssignToPhysicianGroup(physicianGroup);

        // Act
        blockedTime.AssignToPhysicianGroup(physicianGroup);

        // Assert
        Assert.Single(blockedTime.PhysicianGroup);
    }
    
    // BlocksAll tests
    [Fact]
    public void BlocksAll_ShouldSetBlocksAllPhysiciansToTrue() {
        // Arrange
        var blockedTime = new BlockedTime(BlockedTimeType.Vacation, new TimeOnly(9, 0), new TimeOnly(17, 0));
        // Act
        blockedTime.BlocksAll();
        // Assert
        Assert.True(blockedTime.BlocksAllPhysicians);
    }
    
    // RecurWeekly tests
    [Fact]
    public void RecurWeekly_ShouldSetRecurrenceProperties_WhenCalledWithValidDaysAndInterval() {
        // Arrange
        var startTime = new TimeOnly(9, 0);
        var endTime = new TimeOnly(17, 0);
        var startDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1));
        var blockedTime = new BlockedTime(BlockedTimeType.Vacation, startTime, endTime, startDate);
        List<DayOfWeek> daysOfWeek = [DayOfWeek.Monday, DayOfWeek.Wednesday];

        // Act
        blockedTime.RecurWeekly(daysOfWeek, 2);

        // Assert
        Assert.Equal(RecurrenceType.Weekly, blockedTime.RecurrenceType);
        Assert.Equal(2, blockedTime.RecurrenceInterval);
        Assert.Equal(daysOfWeek, blockedTime.RecurrenceDays);
    }

    [Fact]
    public void RecurWeekly_ShouldThrowArgumentOutOfRangeException_WhenIntervalIsNotPositive() {
        // Arrange
        var startTime = new TimeOnly(9, 0);
        var endTime = new TimeOnly(17, 0);
        var startDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1));
        var blockedTime = new BlockedTime(BlockedTimeType.Vacation, startTime, endTime, startDate);
        
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => blockedTime.RecurWeekly([DayOfWeek.Monday], 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => blockedTime.RecurWeekly([DayOfWeek.Monday], -1));
    }

    [Fact]
    public void RecurWeekly_ShouldThrowArgumentException_WhenDaysOfWeekIsNullOrEmpty() {
        // Arrange
        var startTime = new TimeOnly(9, 0);
        var endTime = new TimeOnly(17, 0);
        var startDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1));
        var blockedTime = new BlockedTime(BlockedTimeType.Vacation, startTime, endTime, startDate);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => blockedTime.RecurWeekly(null!));
        Assert.Throws<ArgumentException>(() => blockedTime.RecurWeekly([]));
    }
    
    [Fact]
    public void UpdateRecurrenceInterval_ShouldUpdateInterval_WhenCalledWithValidInterval() {
        // Arrange
        var startTime = new TimeOnly(9, 0);
        var endTime = new TimeOnly(17, 0);
        var blockedTime = new BlockedTime(BlockedTimeType.AnnualLeave, startTime, endTime);
        
        // Act
        blockedTime.UpdateRecurrenceInterval(3);
        
        // Assert
        Assert.Equal(3, blockedTime.RecurrenceInterval);
    }
    
    [Fact]
    public void UpdateRecurrenceInterval_ShouldThrowArgumentOutOfRangeException_WhenIntervalIsNotPositive() {
        // Arrange
        var startTime = new TimeOnly(9, 0);
        var endTime = new TimeOnly(17, 0);
        var startDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1));
        var blockedTime = new BlockedTime(BlockedTimeType.AnnualLeave, startTime, endTime, startDate);
        
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => blockedTime.UpdateRecurrenceInterval(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => blockedTime.UpdateRecurrenceInterval(-1));
    }
}