using Dd.Domain.Common.Entities;
using Dd.Domain.Reservation.Entities;

namespace Dd.Domain.Test.Reservation.Entities;

public class TimeSlotTests {
    private readonly Physician _physician = new();
    private readonly DateOnly _today = DateOnly.FromDateTime(DateTime.UtcNow);
    private readonly TimeOnly _startTime = new TimeOnly(0, 0);
    private readonly TimeSpan _span = new TimeSpan(1, 0, 0);
    // constructor tests
    [Fact]
    public void TimeSlot_Constructor_ShouldInitializeProperties() {
        var timeSlot = new TimeSlot(_physician.Id, _today, _startTime, _span);
        
        Assert.NotNull(timeSlot);
        Assert.Equal(_physician.Id, timeSlot.PhysicianId);
        Assert.Equal(_startTime, timeSlot.StartTime);
        Assert.Equal(_span, timeSlot.Span);
    }
    

    [Fact]
    public void TimeSlot_Constructor_ShouldThrowArgumentOutOfRangeException_WhenGapIsNegative() {
        var span = TimeSpan.FromMinutes(-15);
        Assert.Throws<ArgumentOutOfRangeException>(() => new TimeSlot(_physician.Id, _today, _startTime, span));
    }
}