namespace Dd.Domain.Reservation.Utils;

public class Sequence {
    public int Start { get; private set; }
    public int Interval { get; private set; }
    
    public Sequence(int start, int interval = 1) {
        if (interval <= 0)
            throw new ArgumentOutOfRangeException(nameof(interval), "Interval must be a positive integer.");
        
        Start = start;
        Interval = interval;
    }
    
    public int S(int n) => Start + n * Interval;
}