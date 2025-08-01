using Dd.Domain.Common.Entities;
using Dd.Domain.Reservation.Entities;

namespace Dd.Domain.Test.Reservation.Entities;

public class TimeSlotTests {
    private readonly Physician _physician = new();
    private readonly DateOnly _tomorrow = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1));
    private const int FirstSlot = 0;
    private const int SecondSlot = 1;
    private const int ThreeTicks = 3;

    // constructor tests
    [Fact]
    public void TimeSlot_Constructor_ShouldInitializeProperties() {
        var timeSlot = new TimeSlot(_physician, _tomorrow, FirstSlot, ThreeTicks);
        
        Assert.NotNull(timeSlot);
        Assert.Equal(_physician, timeSlot.Physician);
        Assert.Equal(_physician.Id, timeSlot.PhysicianId);
        Assert.Equal(_tomorrow, timeSlot.Date);
        Assert.Equal(FirstSlot, timeSlot.SlotNumber);
        Assert.Equal(ThreeTicks, timeSlot.Ticks);
    }

    [Fact]
    public void TimeSlot_Constructor_ShouldThrowArgumentNullException_WhenPhysicianIsNull() {
        Assert.Throws<ArgumentNullException>(() => new TimeSlot(null!, _tomorrow, FirstSlot, ThreeTicks));
    }

    [Fact]
    public void TimeSlot_Constructor_ShouldThrowArgumentOutOfRangeException_WhenSlotNumberIsNegative() {
        Assert.Throws<ArgumentOutOfRangeException>(() => new TimeSlot(_physician, _tomorrow, FirstSlot, -1));
    }

    [Fact]
    public void TimeSlot_Constructor_ShouldThrowArgumentOutOfRangeException_WhenTicksIsZeroOrNegative() {
        Assert.Throws<ArgumentOutOfRangeException>(() => new TimeSlot(_physician, _tomorrow, FirstSlot, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => new TimeSlot(_physician, _tomorrow, FirstSlot, -1));
    }

    [Fact]
    public void TimeSlot_Constructor_ShouldThrowArgumentOutOfRangeException_WhenDateIsInThePast() {
        Assert.Throws<ArgumentOutOfRangeException>(() => new TimeSlot(_physician, DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1)), FirstSlot, ThreeTicks));
    }
}