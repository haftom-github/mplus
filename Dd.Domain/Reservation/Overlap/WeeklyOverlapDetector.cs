using Dd.Domain.Reservation.Entities;
using Dd.Domain.Reservation.Enums;
using Dd.Domain.Reservation.Utils;

namespace Dd.Domain.Reservation.Overlap;

public class WeeklyOverlapDetector : BaseOverlapDetector {
    public override bool IsOverlapping(Schedule s1, Schedule s2) {
        var overlap = Detect(s1, s2);
        return overlap.count != 0;
    }

    public override (int? f, int? l, int? count) Detect(Schedule s1, Schedule s2) {
        ArgumentNullException.ThrowIfNull(s1);
        ArgumentNullException.ThrowIfNull(s2);

        if (s1.RecurrenceType != RecurrenceType.Weekly
            || s2.RecurrenceType != RecurrenceType.Weekly)
            throw new ArgumentException("can only detect overlaps between two weekly recurrent schedules");
        
        if (s1.StartTime >= s2.EndTime 
            || s2.StartTime >= s1.EndTime 
            || s1.StartDate > s2.EndDate 
            || s2.StartDate > s1.EndDate)
            return ScheduleMath.NoOverlap;

        var commonDaysOfWeek = s1.RecurrenceDays.Intersect(s2.RecurrenceDays).ToHashSet();
        if (commonDaysOfWeek.Count == 0)
            return ScheduleMath.NoOverlap;

        int? fOverlap = null;
        int? lOverlap = null;
        int? count = 0;
        foreach (var day in commonDaysOfWeek) {
            var s1Start = s1.StartDate.AddDays(0);
            while (s1Start.DayOfWeek != day) s1Start = s1Start.AddDays(1);
            var s2Start = s2.StartDate.AddDays(0);
            while (s2Start.DayOfWeek != day) s2Start = s2Start.AddDays(1);

            var s1Sequence = SequenceFactory.Create(s1Start.DayNumber, s1.EndDate?.DayNumber, s1.RecurrenceInterval * 7);
            var s2Sequence = SequenceFactory.Create(s2Start.DayNumber, s2.EndDate?.DayNumber, s2.RecurrenceInterval * 7);

            var overlap = ScheduleMath.FirstOverlap(s1Sequence, s2Sequence);
            fOverlap ??= overlap.f;
            fOverlap = fOverlap > overlap.f ? overlap.f : fOverlap;
            lOverlap ??= overlap.l;
            lOverlap = lOverlap < overlap.l ? overlap.l : lOverlap;
            count += overlap.count;
        }

        return count switch {
            null => (fOverlap, null, count),
            0 => ScheduleMath.NoOverlap,
            _ => (fOverlap, lOverlap, count)
        };
    }
}