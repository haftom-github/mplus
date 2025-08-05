using Dd.Domain.Reservation.Entities;

namespace Dd.Domain.Common.Entities;

public class Physician : Entity {
    
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    
    private readonly List<Schedule> _schedules = [];
    private readonly List<BlockedTime> _blockedTimes = [];
    public IReadOnlyList<Schedule> Schedules => _schedules.AsReadOnly();
    public IReadOnlyList<BlockedTime> BlockedTimes => _blockedTimes.AsReadOnly();
    
    public void AddSchedule(Schedule schedule) {
        // _schedules.Add(schedule);
    }
    
    public void AddBlockedTime(BlockedTime blockedTime) {
        // _blockedTimes.Add(blockedTime);
    }
    
    public void RemoveSchedule(Schedule schedule) {
        // _schedules.Remove(schedule);
    }
    
    public void RemoveBlockedTime(BlockedTime blockedTime) {
        // _blockedTimes.Remove(blockedTime);
    }
}