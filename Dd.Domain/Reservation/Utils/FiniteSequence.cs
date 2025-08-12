namespace Dd.Domain.Reservation.Utils;

public class FiniteSequence : ISequence {
    public int Start { get; private set; }
    public int End { private set; get; }
    public int Interval { get; private set; }
    public bool IsFinite => true;

    public FiniteSequence(int start, int end, int interval = 1) {
        if (interval <= 0)
            throw new ArgumentOutOfRangeException(nameof(interval), "Interval must be a positive integer.");
        
        var effectiveEnd = start + ((end - start) / interval) * interval;
        if (start >= effectiveEnd)
            throw new ArgumentException($"Start must be less than effective end ({effectiveEnd}).", nameof(start));
        
        Start = start;
        End = effectiveEnd;
        Interval = interval;
    }
    
    public int S(int n) {
        var maxN = (End - Start) / Interval;
        if (n > maxN)
            throw new ArgumentOutOfRangeException(nameof(n), $"n must be less than or equal to {maxN}.");
        
        return Start + n * Interval;
    }
}