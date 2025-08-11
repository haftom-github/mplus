using Dd.Domain.Interfaces;
using Dd.Domain.Reservation.Entities;

namespace Dd.Domain.Reservation.Overlap;

public abstract class BaseOverlapDetector : IOverlapDetector {
    protected internal static DateOnly ToFirstDayOfWeek(DateOnly date) {
        var normalized = date.AddDays(0);
        while (normalized.DayOfWeek != IDateTime.FirstDayOfWeek)
            normalized = date.AddDays(-1);

        return normalized;
    }
    
    protected internal static ISet<DayOfWeek> FirstWeekDifference(Schedule schedule) {
        var firstWeekDifference = new HashSet<DayOfWeek>();
        var date = schedule.StartDate.ToFirstDayOfWeek().AddDays(0);
        while (date < schedule.StartDate) {
            if(schedule.RecurrenceDays.Contains(date.DayOfWeek))
                firstWeekDifference.Add(date.DayOfWeek);
            date = date.AddDays(1);
        }
        return firstWeekDifference;
    }
    
    protected internal static ISet<DayOfWeek> LastWeekDifference(Schedule schedule) {
        if (schedule.EndDate == null) return new HashSet<DayOfWeek>();
        HashSet<DayOfWeek> lastWeekDaysOnSchedule = [];
        var date = schedule.EndDate.Value.ToFirstDayOfWeek().AddDays(0);
        while (date <= schedule.EndDate) {
            if(schedule.RecurrenceDays.Contains(date.DayOfWeek))
                lastWeekDaysOnSchedule.Add(date.DayOfWeek);
            date = date.AddDays(1);
        }
        return schedule.RecurrenceDays.Except(lastWeekDaysOnSchedule).ToHashSet();
    }

    public abstract bool IsOverlapping(Schedule schedule1, Schedule schedule2);
}


public static class ScheduleExtensions {
    public static DateOnly ToFirstDayOfWeek(this DateOnly date) {
        return BaseOverlapDetector.ToFirstDayOfWeek(date);
    }
    
    public static ISet<DayOfWeek> FirstWeekDifference(this Schedule schedule) {
        return BaseOverlapDetector.FirstWeekDifference(schedule);
    }
    
    public static ISet<DayOfWeek> LastWeekDifference(this Schedule schedule) {
        return BaseOverlapDetector.LastWeekDifference(schedule);
    }
}