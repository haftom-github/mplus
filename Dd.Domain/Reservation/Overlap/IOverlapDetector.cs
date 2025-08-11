using Dd.Domain.Reservation.Entities;

namespace Dd.Domain.Reservation.Overlap;

public interface IOverlapDetector {
    public bool IsOverlapping(Schedule schedule1, Schedule schedule2);
}