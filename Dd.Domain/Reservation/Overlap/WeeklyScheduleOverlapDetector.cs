using Dd.Domain.Reservation.Entities;
using Dd.Domain.Reservation.Utils;

namespace Dd.Domain.Reservation.Overlap;

public class WeeklyScheduleOverlapDetector : BaseOverlapDetector {
    public override bool IsOverlapping(Schedule s1, Schedule s2) {
        ArgumentNullException.ThrowIfNull(s1);
        ArgumentNullException.ThrowIfNull(s2);
        
        if (s1.StartTime >= s2.EndTime || s2.StartTime >= s1.EndTime)
            return false;
        
        if (s1.StartDate > s2.EndDate || s2.StartDate > s1.EndDate)
            return false;

        var commonDaysOfWeek = s1.RecurrenceDays.Intersect(s2.RecurrenceDays).ToHashSet();
        if (commonDaysOfWeek.Count == 0)
            return false;

        foreach (var day in commonDaysOfWeek) {
            var s1Start = s1.StartDate.AddDays(0);
            while (s1Start.DayOfWeek != day) s1Start = s1Start.AddDays(1);
            var s2Start = s2.StartDate.AddDays(0);
            while (s2Start.DayOfWeek != day) s2Start = s2Start.AddDays(1);

            var s1Sequence = s1.EndDate == null
                ? new Sequence(s1Start.DayNumber, s1.RecurrenceInterval * 7)
                : new FiniteSequence(s1Start.DayNumber, s1.EndDate.Value.DayNumber, s1.RecurrenceInterval * 7);
            
            var s2Sequence = s2.EndDate == null
                ? new Sequence(s2Start.DayNumber, s2.RecurrenceInterval * 7)
                : new FiniteSequence(s2Start.DayNumber, s2.EndDate.Value.DayNumber, s2.RecurrenceInterval * 7);

            if (ScheduleMath.Overlaps(s1Sequence, s2Sequence)) return true;
        }

        return false;
    }
}