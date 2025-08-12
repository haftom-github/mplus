using Dd.Domain.Reservation.Entities;
using Dd.Domain.Reservation.Utils;

namespace Dd.Domain.Reservation.Overlap;

public interface IOverlapDetector {
    public bool IsOverlapping(Schedule s1, Schedule s2);
    public ISequence? Detect(Schedule s1, Schedule s2);
}