using Dd.Domain.Reservation.Entities;
using Dd.Domain.Reservation.Enums;
using Dd.Domain.Reservation.Utils;

namespace Dd.Domain.Reservation.Overlap;

public class WeeklyVsDailyOverlapDetector : BaseOverlapDetector {
    public override bool IsOverlapping(Schedule s1, Schedule s2) {
        ArgumentNullException.ThrowIfNull(s1);
        ArgumentNullException.ThrowIfNull(s2);
        if (s1.RecurrenceType != RecurrenceType.Weekly)
            (s1, s2) = (s2, s1);
        
        if (s1.RecurrenceType != RecurrenceType.Weekly)
            throw new ArgumentException("one of the schedules must be weekly");
        
        if (s1.StartTime >= s2.EndTime || s2.StartTime >= s1.EndTime)
            return false;
        
        if (s1.StartDate > s2.EndDate || s2.StartDate > s1.EndDate)
            return false;

        var s2Sequence = s2.EndDate == null
            ? new Sequence(s2.StartDate.DayNumber, s2.RecurrenceInterval * 7)
            : new FiniteSequence(s2.StartDate.DayNumber, s2.EndDate.Value.DayNumber, s2.RecurrenceInterval * 7);
        
        foreach (var day in s1.RecurrenceDays) {
            var s1Start = s1.StartDate.AddDays(0);
            while (s1Start.DayOfWeek != day) s1Start = s1Start.AddDays(1);

            var s1Sequence = s1.EndDate == null
                ? new Sequence(s1Start.DayNumber, s1.RecurrenceInterval * 7)
                : new FiniteSequence(s1Start.DayNumber, s1.EndDate.Value.DayNumber, s1.RecurrenceInterval * 7);
            
            if (ScheduleMath.Overlaps(s1Sequence, s2Sequence)) return true;
        }

        return false;
    }
}