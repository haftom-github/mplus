using Dd.Domain.Interfaces;
using Dd.Domain.Reservation.Entities;
using Dd.Domain.Reservation.Enums;
using Dd.Domain.Reservation.Utils;

namespace Dd.Domain.Reservation.Overlap;

public class WeeklyOverlapDetector : BaseOverlapDetector {
    protected override ISequence? SplitDetect(Schedule s1, Schedule s2) {
        if (s1.RecurrenceType != RecurrenceType.Weekly
            || s2.RecurrenceType != RecurrenceType.Weekly)
            throw new ArgumentException("can only detect overlaps between two weekly recurrent schedules");

        if (OverlapImpossible(s1, s2)) return null;

        var commonDaysOfWeek = s1.RecurrenceDays.Intersect(s2.RecurrenceDays).ToHashSet();
        if (commonDaysOfWeek.Count == 0)
            return null;
        
        var s1Start = s1.StartDate.ToFirstDayOfWeek();
        var s2Start = s2.StartDate.ToFirstDayOfWeek();
        
        while (s1Start.AddDays(1).DayOfWeek != IDateTime.FirstDayOfWeek 
               && !commonDaysOfWeek.Contains(s1Start.DayOfWeek))
            (s1Start, s2Start) = (s1Start.AddDays(1), s2Start.AddDays(1));
        
        var s1Sequence = SequenceFactory.Create(s1Start.DayNumber, s1.EndDate?.DayNumber, s1.RecurrenceInterval * 7);
        var s2Sequence = SequenceFactory.Create(s2Start.DayNumber, s2.EndDate?.DayNumber, s2.RecurrenceInterval * 7);
        var overlap = ScheduleMath.FirstOverlap(s1Sequence, s2Sequence);
        if (overlap == null) return null;
        return overlap.Start < s1.StartDate.DayNumber 
               || overlap.Start < s2.StartDate.DayNumber ? overlap.StartFromNext() : overlap;
    }
}