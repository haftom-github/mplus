using Dd.Domain.Common.Entities;
using Dd.Domain.Reservation.Entities;

namespace Dd.Domain.Test.Reservation.Entities;

public class TimeSlotTests {
    private readonly Physician _physician = new();
    private const int FirstSlot = 0;
    private const int SecondSlot = 1;
    private const int ThreeTicks = 3;

    // constructor tests
    [Fact]
    public void TimeSlot_Constructor_ShouldInitializeProperties() {
        var timeSlot = new TimeSlot(_physician.Id, FirstSlot, ThreeTicks);
        
        Assert.NotNull(timeSlot);
        Assert.Equal(_physician.Id, timeSlot.PhysicianId);
        Assert.Equal(FirstSlot, timeSlot.SlotNumber);
        Assert.Equal(ThreeTicks, timeSlot.Ticks);
        Assert.Equal(TimeSpan.Zero, timeSlot.Gap);
    }

    [Fact]
    public void TimeSlot_Constructor_ShouldThrowArgumentOutOfRangeException_WhenSlotNumberIsNegative() {
        Assert.Throws<ArgumentOutOfRangeException>(() => new TimeSlot(_physician.Id, FirstSlot, -1));
    }

    [Fact]
    public void TimeSlot_Constructor_ShouldThrowArgumentOutOfRangeException_WhenTicksIsZeroOrNegative() {
        Assert.Throws<ArgumentOutOfRangeException>(() => new TimeSlot(_physician.Id, FirstSlot, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => new TimeSlot(_physician.Id, FirstSlot, -1));
    }

    [Fact]
    public void TimeSlot_Constructor_ShouldInitializeWithGap_WhenProvided() {
        var gap = TimeSpan.FromMinutes(15);
        var timeSlot = new TimeSlot(_physician.Id, FirstSlot, ThreeTicks, gap);
        
        Assert.Equal(gap, timeSlot.Gap);
    }

    [Fact]
    public void TimeSlot_Constructor_ShouldThrowArgumentOutOfRangeException_WhenGapIsNegative() {
        var gap = TimeSpan.FromMinutes(-15);
        Assert.Throws<ArgumentOutOfRangeException>(() => new TimeSlot(_physician.Id, FirstSlot, ThreeTicks, gap));
    }
}