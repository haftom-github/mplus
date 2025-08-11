using Dd.Domain.Reservation.Enums;

namespace Dd.Domain.Reservation.Overlap;

public static class OverlapDetectorFactory {
    public static IOverlapDetector Create(RecurrenceType recurrenceType) {
        return recurrenceType switch {
            RecurrenceType.Daily => new DailyScheduleOverlapDetector(),
            RecurrenceType.Weekly => new WeeklyScheduleOverlapDetector(),
            _ => throw new ArgumentOutOfRangeException(nameof(recurrenceType), recurrenceType, null),
        };
    }
}