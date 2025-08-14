using Dd.Domain.Reservation.Entities;
using Dd.Domain.Reservation.Overlap;

namespace Dd.Domain.Test.Reservation.Overlap;

public class ScheduleExtensionsTests {
    private readonly DateOnly _today = DateOnly.FromDateTime(DateTime.UtcNow);
    [Fact]
    public void Split_ShouldReturnTheScheduleAndNull_WhenScheduleDoesNotCrossDayBoundary() {
        var startTime = TimeOnly.MinValue;
        var endTime = TimeOnly.MaxValue;
        var schedule = new Schedule(startTime, endTime, _today);

        var (s, e) = schedule.Split();
        
        Assert.Equal(schedule, s);
        Assert.Null(e);
    }

    [Fact]
    public void Split_ShouldOnlyChangeEndTimeToGetS_WhenScheduleCrossesDayBoundary() {
        var startTime = new TimeOnly(20, 0);
        var endTime = new TimeOnly(4, 0);
        var schedule = new Schedule(startTime, endTime, _today);
        
        var (s, e) = schedule.Split();
        
        Assert.Equal(s.StartTime, startTime);
        Assert.Equal(s.EndTime, TimeOnly.MaxValue);
        Assert.Equal(s.RecurrenceType, schedule.RecurrenceType);
        Assert.Equal(s.StartDate, schedule.StartDate);
        Assert.Equal(s.EndDate, schedule.EndDate);
        Assert.Equal(s.RecurrenceDays, schedule.RecurrenceDays);
        
        schedule.RecurWeekly([DayOfWeek.Monday], 7);
        (s, e) = schedule.Split();
        Assert.Equal(s.RecurrenceDays, schedule.RecurrenceDays);
        Assert.Equal(s.RecurrenceInterval, schedule.RecurrenceInterval);
    }
    
    [Fact]
    public void Split_ShouldUpdateStartTimeToGetE_WhenScheduleCrossesDayBoundary() {
        var startTime = new TimeOnly(20, 0);
        var endTime = new TimeOnly(4, 0);
        var schedule = new Schedule(startTime, endTime, _today);
        
        var (s, e) = schedule.Split();

        Assert.NotNull(e);
        Assert.Equal(e.StartTime, TimeOnly.MinValue);
        Assert.Equal(e.EndTime, schedule.EndTime);
        Assert.Equal(e.RecurrenceType, schedule.RecurrenceType);
        Assert.Equal(e.StartDate, schedule.StartDate.AddDays(1));
        Assert.Equal(e.EndDate, schedule.EndDate?.AddDays(1));
        Assert.Equal(e.RecurrenceDays, schedule.RecurrenceDays);

        schedule.RecurWeekly([DayOfWeek.Sunday, DayOfWeek.Monday], 7);
        (s, e) = schedule.Split();
        
        Assert.NotNull(e);
        Assert.Equal(e.RecurrenceDays, [DayOfWeek.Monday, DayOfWeek.Tuesday]);
    }
}