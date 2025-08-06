namespace Dd.Domain.Reservation.Utils;

public class Sequence {
    public int Start { get; private set; }
    public int End { get; private set; }
    public int Interval { get; private set; }
    
    public Sequence(int start, int end, int interval = 1) {
        var effectiveEnd = start + ((end - start) / interval) * interval;
        if (start >= effectiveEnd)
            throw new ArgumentException($"Start must be less than effective end ({effectiveEnd}).", nameof(start));
        
        if (interval <= 0)
            throw new ArgumentOutOfRangeException(nameof(interval), "Interval must be a positive integer.");
        
        Start = start;
        End = effectiveEnd;
        Interval = interval;
    }
}