namespace Dd.Domain.Reservation.Utils;

public class Sequence {
    public int Start { get; private set; }
    public int End { get; private set; }
    public int Interval { get; private set; }
    
    public Sequence(int start, int end, int interval = 1) {
        if (start >= end)
            throw new ArgumentException("Start must be less than end.", nameof(start));
        
        if (interval <= 0)
            throw new ArgumentOutOfRangeException(nameof(interval), "Interval must be a positive integer.");
        
        Start = start;
        End = end;
        Interval = interval;
    }
}