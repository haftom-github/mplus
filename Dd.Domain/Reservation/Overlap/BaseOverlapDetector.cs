using Dd.Domain.Interfaces;
using Dd.Domain.Reservation.Entities;
using Dd.Domain.Reservation.Utils;

namespace Dd.Domain.Reservation.Overlap;

public abstract class BaseOverlapDetector : IOverlapDetector {
    protected bool MayOverlap(Schedule s1, Schedule s2) {
        if (s1.StartDate > s2.EndDate?.AddDays(1) ||
                s2.StartDate > s1.EndDate?.AddDays(1)) 
            return false;

        var s1Prior = s1.PriorMidnight();
        var s1Post = s1.PostMidnight();
        var s2Prior = s2.PriorMidnight();
        var s2Post = s2.PostMidnight();
        return Overlaps(s1Prior, s2Prior) 
               || Overlaps(s1Prior, s2Post) 
               || Overlaps(s1Post, s2Prior) 
               || Overlaps(s1Post, s2Post);
    }

    private static bool Overlaps((TimeOnly s, TimeOnly e)? s1, (TimeOnly s, TimeOnly e)? s2)
        => s1?.s < s2?.e && s2?.s < s1?.e;
    
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

    public static (TimeOnly s, TimeOnly e) PriorMidnight(this Schedule s) =>
        s.StartTime > s.EndTime 
            ? (s.StartTime, TimeOnly.MaxValue) 
            : (s.StartTime, s.EndTime);
    
    public static (TimeOnly s, TimeOnly e)? PostMidnight(this Schedule s) => 
        s.StartTime > s.EndTime 
            ? (TimeOnly.MinValue, s.EndTime) 
            : null;
}