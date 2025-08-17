using Dd.Domain.Schedules.Entities;
using Dd.Domain.Schedules.Sequences;

namespace Dd.Domain.Schedules.Overlap;

public interface IOverlapDetector {
    public bool IsOverlapping(Schedule s1, Schedule s2);
    public ISequence? Detect(Schedule s1, Schedule s2);
}