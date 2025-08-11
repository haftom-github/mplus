using Dd.Domain.Reservation.Entities;
using Dd.Domain.Reservation.Utils;

namespace Dd.Domain.Reservation.Overlap;

public class DailyScheduleOverlapDetector : BaseOverlapDetector {
    public override bool IsOverlapping(Schedule s1, Schedule s2) {
        ArgumentNullException.ThrowIfNull(s1);
        ArgumentNullException.ThrowIfNull(s2);
        
        if (s1.StartTime >= s2.EndTime || s2.StartTime >= s1.EndTime)
            return false;
        
        if (s1.StartDate > s2.EndDate || s2.StartDate > s1.EndDate)
            return false;
        
        if (s1.RecurrenceInterval == 1 || s2.RecurrenceInterval == 1)
            return true;
        
        var s1Sequence = s1.EndDate == null 
            ? new Sequence(s1.StartDate.DayNumber, s1.RecurrenceInterval)
            : new FiniteSequence(s1.StartDate.DayNumber, s1.EndDate.Value.DayNumber, s1.RecurrenceInterval);
        
        var s2Sequence = s2.EndDate == null 
            ? new Sequence(s2.StartDate.DayNumber, s2.RecurrenceInterval)
            : new FiniteSequence(s2.StartDate.DayNumber, s2.EndDate.Value.DayNumber, s2.RecurrenceInterval);
        
        return ScheduleMath.Overlaps(s1Sequence, s2Sequence);
    }
}