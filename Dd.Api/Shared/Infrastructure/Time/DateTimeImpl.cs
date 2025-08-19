using Dd.Api.Shared.Domain.Time;

namespace Dd.Api.Shared.Infrastructure.Time;

public class DateTimeImpl : IDateTime {
    public DayOfWeek FirstDayOfWeek() {
        return DayOfWeek.Monday;
    }
    public DateTime Now() {
        return DateTime.UtcNow;
    }
}