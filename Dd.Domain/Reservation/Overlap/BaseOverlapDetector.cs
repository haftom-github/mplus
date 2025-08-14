using Dd.Domain.Interfaces;
using Dd.Domain.Reservation.Entities;
using Dd.Domain.Reservation.Utils;

namespace Dd.Domain.Reservation.Overlap;

public abstract class BaseOverlapDetector : IOverlapDetector {
    protected bool OverlapImpossible(Schedule s1, Schedule s2) {
        return s1.StartTime >= s2.EndTime 
               || s2.StartTime >= s1.EndTime 
               || s1.StartDate > s2.EndDate
               || s2.StartDate > s1.EndDate;
    }

    protected internal static DateOnly ToFirstDayOfWeek(DateOnly date) {
        var normalized = date.AddDays(0);
        while (normalized.DayOfWeek != IDateTime.FirstDayOfWeek)
            normalized = normalized.AddDays(-1);

        return normalized;
    }

    public abstract bool IsOverlapping(Schedule schedule1, Schedule schedule2);
    public abstract ISequence? Detect(Schedule s1, Schedule s2);
}

public static class ScheduleExtensions {
    public static DateOnly ToFirstDayOfWeek(this DateOnly date) {
        return BaseOverlapDetector.ToFirstDayOfWeek(date);
    }
}