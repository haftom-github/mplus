using Dd.Domain.Reservation.Entities;
using Dd.Domain.Reservation.Utils;

namespace Dd.Domain.Reservation.Overlap;

public class DailyOverlapDetector : BaseOverlapDetector {
    public override bool IsOverlapping(Schedule s1, Schedule s2) {
        return Detect(s1, s2) != null;
    }

    public override ISequence? Detect(Schedule s1, Schedule s2) {
        ArgumentNullException.ThrowIfNull(s1);
        ArgumentNullException.ThrowIfNull(s2);
        
        // if (s1.StartTime >= s2.EndTime 
        //     || s2.StartTime >= s1.EndTime 
        //     || s1.StartDate > s2.EndDate || 
        //     s2.StartDate > s1.EndDate)
        //     return null;

        if (OverlapImpossible(s1, s2)) return null;

        var s1Sequence = SequenceFactory.Create(s1.StartDate.DayNumber, s1.EndDate?.DayNumber, s1.RecurrenceInterval);
        
        var s2Sequence = SequenceFactory.Create(s2.StartDate.DayNumber, s2.EndDate?.DayNumber, s2.RecurrenceInterval);
        
        return ScheduleMath.FirstOverlap(s1Sequence, s2Sequence);
    }
}