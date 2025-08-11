using Dd.Domain.Reservation.Entities;

namespace Dd.Domain.Reservation.Overlap;

public abstract class BaseOverlapDetector : IOverlapDetector {
    protected const DayOfWeek StartOfWeek = DayOfWeek.Sunday;
    protected internal static DateOnly Normalize(DateOnly date, DayOfWeek startOfWeek) {
        var normalized = date.AddDays(0);
        while (normalized.DayOfWeek != startOfWeek)
            normalized = date.AddDays(-1);

        return normalized;
    }
    
    protected internal static ISet<DayOfWeek> FirstWeekDifference(Schedule schedule) {
        var firstWeekDifference = new HashSet<DayOfWeek>();
        var date = schedule.StartDate.ToStartOfWeek(StartOfWeek).AddDays(0);
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
        var date = schedule.EndDate.Value.ToStartOfWeek(StartOfWeek).AddDays(0);
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
    public static DateOnly ToStartOfWeek(this DateOnly date, DayOfWeek startOfWeek) {
        return BaseOverlapDetector.Normalize(date, startOfWeek);
    }
    
    public static ISet<DayOfWeek> FirstWeekDifference(this Schedule schedule) {
        return BaseOverlapDetector.FirstWeekDifference(schedule);
    }
    
    public static ISet<DayOfWeek> LastWeekDifference(this Schedule schedule) {
        return BaseOverlapDetector.LastWeekDifference(schedule);
    }
}