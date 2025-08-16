using Dd.Domain.Reservation.Entities;
using Dd.Domain.Reservation.Enums;
using Dd.Domain.Reservation.Services;
using TimeOnly = System.TimeOnly;

namespace Dd.Domain.Test.Reservation.Services;

public class SlotServiceTests {

    private readonly Guid _physicianId;
    private readonly DateOnly _today;
    private readonly TimeOnly _2OClock;
    private readonly TimeOnly _11OClock;
    private readonly TimeOnly _10OClockMorning;
    private readonly DateOnly _lastMonth;
    private readonly WorkSchedule _officeHours;
    private readonly WorkSchedule _nightShift;
    private readonly BlockedSchedule _weekend;
    private readonly BlockedSchedule _lunchBreak;
    private readonly BlockedSchedule _coffeeBreak;

    public SlotServiceTests() {
        _physicianId = Guid.NewGuid();
        _today = DateOnly.FromDateTime(DateTime.UtcNow);
        _2OClock = new TimeOnly(5, 0);
        _11OClock = new TimeOnly(14, 0);
        _10OClockMorning = new TimeOnly(1, 0);
        _lastMonth = _today.AddDays(-30);
        _officeHours = new WorkSchedule(_2OClock, _11OClock, _lastMonth);
        _nightShift = new WorkSchedule(_11OClock, _10OClockMorning, _lastMonth);
        _weekend = new BlockedSchedule(BlockedScheduleType.WeeklyBreak, _lastMonth, null, null);
        _weekend.RecurWeekly([DayOfWeek.Sunday, DayOfWeek.Saturday]);
        var luchStart = new TimeOnly(10, 0);
        var lunchEnd = new TimeOnly(11, 0);
        _lunchBreak = new BlockedSchedule(BlockedScheduleType.LunchBreak, _lastMonth, luchStart, lunchEnd);
        var coffeeStart = new TimeOnly(6, 0);
        var coffeeEnd = new TimeOnly(6, 30);
        _coffeeBreak = new BlockedSchedule(BlockedScheduleType.CoffeeBreak, _lastMonth, coffeeStart, coffeeEnd);
    }
    
    [Fact]
    public void Generate_ShouldReturnEmptyList_WhenDayIsCompletelyBlocked() {
        var availableSlots = 
            SlotService.Generate(_physicianId, _today, [_officeHours], [_weekend]);
        
        Assert.Empty(availableSlots);
    }

    [Fact]
    public void Generate_ShouldReturnASingleSlot_WhenDayIsCompletelyFree() {
        var availableSlots = 
            SlotService.Generate(_physicianId, _today.AddDays(2), [_officeHours], [_weekend]);
        
        Assert.Single(availableSlots);
        Assert.Equal(availableSlots.First().StartTime, _2OClock);
        Assert.Equal(availableSlots.First().Span, _11OClock - _2OClock);
    }

    [Fact]
    public void Generate_ShouldHaveNoEffect_WhenAddingTwoOverlappingBlockingTimes() {
        var slots = SlotService.Generate(_physicianId, _today, [_officeHours], [_weekend]);
        var slots2 = SlotService.Generate(_physicianId, _today, [_officeHours], [_weekend, _lunchBreak]);
        
        Assert.Equal(slots2.Count, slots.Count);
        Assert.Empty(slots);
    }
    
    [Fact]
    public void Generate_WhenTwoBlockingSchedules() {
        var slots = SlotService.Generate(_physicianId, _today.AddDays(2), [_officeHours], [_weekend, _lunchBreak]);
        
        Assert.Equal(2, slots.Count);
        // Assert.Equal();
    }

    [Fact]
    public void Generate_WhenThreeBlockingSchedules() {
        var slots = 
            SlotService.Generate(_physicianId,
                _today.AddDays(2), 
                [_officeHours], 
                [_weekend, _lunchBreak, _coffeeBreak]);
        
        Assert.Equal(3, slots.Count);
        Assert.Equal(_2OClock, slots[0].StartTime);
        Assert.Equal(new TimeOnly(6, 30), slots[1].StartTime);
        Assert.Equal(new TimeOnly(11, 0), slots[2].StartTime);
        Assert.Equal(_11OClock, slots[2].StartTime.Add(slots[2].Span));
    }

    [Fact]
    public void Generate_WhenScheduleCrossesBoundary() {
        _nightShift.UpdateRecurrenceInterval(10);
        var slots = SlotService.Generate(_physicianId, _today.AddDays(1), [_nightShift], []);
        
        Assert.NotEmpty(slots);
    }

    [Fact]
    public void Generate_WhenMoreThanOneSchedule() {
        var schedule1 = new WorkSchedule(_2OClock, _11OClock, _lastMonth);
        schedule1.RecurWeekly([DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday]);

        var saturdays = new WorkSchedule(_2OClock, new TimeOnly(9, 0), _lastMonth);
        saturdays.RecurWeekly([DayOfWeek.Saturday]);

        var slots = SlotService.Generate(_physicianId, _today, [schedule1, saturdays], [_coffeeBreak, _lunchBreak]);
        
        Assert.NotEmpty(slots);
    }
}