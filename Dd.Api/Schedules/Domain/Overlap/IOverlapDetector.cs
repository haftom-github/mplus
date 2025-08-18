using Dd.Api.Schedules.Domain.Entities;
using Dd.Domain.Schedules.Sequences;

namespace Dd.Api.Schedules.Domain.Overlap;

public interface IOverlapDetector {
    public bool IsOverlapping(Schedule s1, Schedule s2);
    public ISequence? Detect(Schedule s1, Schedule s2);
}