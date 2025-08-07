namespace Dd.Domain.Reservation.Utils;

public class FiniteSequence : Sequence {
    public int End { get; private set; }
    
    public FiniteSequence(int start, int end, int interval = 1) : base(start, interval) {
        var effectiveEnd = start + ((end - start) / interval) * interval;
        if (start >= effectiveEnd)
            throw new ArgumentException($"Start must be less than effective end ({effectiveEnd}).", nameof(start));
        
        End = effectiveEnd;
    }
}