using Dd.Domain.Reservation.Entities;
using Dd.Domain.Reservation.Enums;
using Dd.Domain.Reservation.Utils;

namespace Dd.Domain.Reservation.Overlap;

public class WeeklyOverlapDetector : BaseOverlapDetector {
    public override bool IsOverlapping(Schedule s1, Schedule s2) {
        return Detect(s1, s2) != null;
    }

    public override ISequence? Detect(Schedule s1, Schedule s2) {
        ArgumentNullException.ThrowIfNull(s1);
        ArgumentNullException.ThrowIfNull(s2);

        if (s1.RecurrenceType != RecurrenceType.Weekly
            || s2.RecurrenceType != RecurrenceType.Weekly)
            throw new ArgumentException("can only detect overlaps between two weekly recurrent schedules");
        
        if (s1.StartTime >= s2.EndTime 
            || s2.StartTime >= s1.EndTime 
            || s1.StartDate > s2.EndDate 
            || s2.StartDate > s1.EndDate)
            return null;

        var commonDaysOfWeek = s1.RecurrenceDays.Intersect(s2.RecurrenceDays).ToHashSet();
        if (commonDaysOfWeek.Count == 0)
            return null;

        foreach (var day in commonDaysOfWeek) {
            var s1Start = s1.StartDate.AddDays(0);
            while (s1Start.DayOfWeek != day) s1Start = s1Start.AddDays(1);
            var s2Start = s2.StartDate.AddDays(0);
            while (s2Start.DayOfWeek != day) s2Start = s2Start.AddDays(1);

            var s1Sequence = SequenceFactory.Create(s1Start.DayNumber, s1.EndDate?.DayNumber, s1.RecurrenceInterval * 7);
            var s2Sequence = SequenceFactory.Create(s2Start.DayNumber, s2.EndDate?.DayNumber, s2.RecurrenceInterval * 7);

            var overlap = ScheduleMath.FirstOverlap(s1Sequence, s2Sequence);
            if (overlap != null) return overlap;
        }

        return null;
    }
}