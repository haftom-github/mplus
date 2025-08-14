using Dd.Domain.Reservation.Entities;
using Period = System.ValueTuple<System.TimeOnly, System.TimeSpan>;

namespace Dd.Domain.Reservation.Services;

public static class SlotService {
    public static List<TimeSlot> Generate(DateOnly date, List<WorkSchedule> workSchedules, List<BlockedSchedule> blockedSchedules) {
        throw new NotImplementedException();
    }

    private static List<Period> Generate(List<Period> working, List<Period> blocking) {
        throw new NotImplementedException();
    }
}