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

    public abstract bool IsOverlapping(Schedule schedule1, Schedule schedule2);
    public abstract (int? f, int? l, int? count) Detect(Schedule s1, Schedule s2);
}

public static class ScheduleExtensions {
    public static DateOnly ToFirstDayOfWeek(this DateOnly date) {
        return BaseOverlapDetector.ToFirstDayOfWeek(date);
    }
}