using Dd.Domain.Reservation.Entities;

namespace Dd.Domain.Reservation.Overlap;

public interface IOverlapDetector {
    public bool IsOverlapping(Schedule s1, Schedule s2);
    public (int? f, int? l, int? count) Detect(Schedule s1, Schedule s2);
}