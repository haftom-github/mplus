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

        var commonDaysOfWeek = s1.RecurrenceDays.Intersect(s2.RecurrenceDays);
        if (!commonDaysOfWeek.Any())
            return false;

        var s1FirstWeek = s1.StartDate.ToStartOfWeek(StartOfWeek);
        var s2FirstWeek = s2.StartDate.ToStartOfWeek(StartOfWeek);
        
        var s1Sequence = s1.EndDate == null
            ? new Sequence(s1FirstWeek.DayNumber, s1.RecurrenceInterval * 7)
            : new FiniteSequence(s2FirstWeek.DayNumber, s1.EndDate.Value.DayNumber, s1.RecurrenceInterval * 7);
        
        var s2Sequence = s2.EndDate == null
            ? new Sequence(s2FirstWeek.DayNumber, s2.RecurrenceInterval * 7)
            : new FiniteSequence(s2FirstWeek.DayNumber, s2.EndDate.Value.DayNumber, s2.RecurrenceInterval * 7);
        
        // var overlaps = ScheduleMath.FirstOverlap(s1Sequence, s2Sequence);
        // if (overlaps == null) return false;
        // if (overlaps?.count is null or > 2) return true;
        // if (s1FirstWeek.DayNumber != s2FirstWeek.DayNumber)
        //     return true;
        // if (overlaps?.f != s1FirstWeek.DayNumber)
        //     return true;
        // if (s1.EndDate != null) { }
        return false;
    }
}