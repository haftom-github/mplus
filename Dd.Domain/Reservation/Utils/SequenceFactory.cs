namespace Dd.Domain.Reservation.Utils;

public static class SequenceFactory {
    public static Sequence Create(int start, int? end, int interval) {
        return end switch {
            null => new Sequence(start, interval),
            _ => new FiniteSequence(start, end.Value, interval)
        };
    }
}