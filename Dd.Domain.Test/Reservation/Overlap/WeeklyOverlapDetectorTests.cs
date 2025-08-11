using Dd.Domain.Reservation.Entities;
using Dd.Domain.Reservation.Overlap;

namespace Dd.Domain.Test.Reservation.Overlap;

public class WeeklyOverlapDetectorTests {
    private readonly WeeklyOverlapDetector _overlapDetector = new WeeklyOverlapDetector();
    private readonly DateOnly _today = DateOnly.FromDateTime(DateTime.UtcNow);
    
    //no overlap cases
    [Fact]
    public void Detect_ShouldThrowAnException_WhenNullArgumentProvided() {
        var schedule1 = new Schedule(new TimeOnly(0, 0), new TimeOnly(1, 0), _today);
        schedule1.RecurWeekly([DayOfWeek.Monday]);
        Assert.Throws<ArgumentNullException>(() => _overlapDetector.Detect(null, schedule1));
        Assert.Throws<ArgumentNullException>(() => _overlapDetector.Detect(schedule1, null));
        Assert.Throws<ArgumentNullException>(() => _overlapDetector.Detect(null, null));
    }
    
    [Fact]
    public void Detect_ShouldThrowArgumentException_WhenOneOfTheSchedulesAreNotWeekly() {
        var schedule1 = new Schedule(new TimeOnly(0, 0), new TimeOnly(1, 0), _today);
        var schedule2 = new Schedule(new  TimeOnly(0, 0), new TimeOnly(1, 0), _today);
        schedule2.RecurWeekly([DayOfWeek.Monday]);
        Assert.Throws<ArgumentException>(() => _overlapDetector.Detect(schedule1, schedule2));
    }

    [Fact]
    public void Detect_ShouldReturnNoOverlap_WhenNonOverlappingTimeRangeDetected() {
        var schedule1 = new Schedule(new TimeOnly(0, 0), new TimeOnly(1, 0), _today);
        var schedule2 = new Schedule(new  TimeOnly(1, 0), new TimeOnly(2, 0), _today);
        
        schedule1.RecurWeekly([DayOfWeek.Monday]);
        schedule2.RecurWeekly([DayOfWeek.Monday]);
        
        var result = _overlapDetector.Detect(schedule1, schedule2);
        Assert.Equal(0, result.count);
        Assert.Null(result.f);
        Assert.Null(result.l);
    }
}