namespace Dd.Domain.Reservation.Utils;

public static class ScheduleMath {
    public static int Gcd(int a, int b) {
        while (a != 0) {
            var temp = a;
            a = b % a;
            b = temp;
        }
        return b;
    }
    
    public static (int gcd, int x, int y) ExtendedGcd(int a, int b) {
        if (a == 0) 
            return (b, 0, 1);
        
        var (gcd, x1, y1) = ExtendedGcd(b % a, a);
        return (gcd, y1 - (b / a) * x1, x1);
    }
    
    /// <summary>
    /// checks if two sequences meet.
    /// </summary>
    /// <param name="a0"> first sequence start</param>
    /// <param name="d1"> first sequence interval</param>
    /// <param name="b0"> second sequence start</param>
    /// <param name="d2"> second sequence interval</param>
    /// <returns> true if the sequences have a possibility of overlapping </returns>
    public static bool Overlaps(int a0, int d1, int b0, int d2) {
        return (a0 - b0) % Gcd(d1, d2) == 0;
    }
    

    /// <summary>
    /// checks if two finite sequences meet.
    /// </summary>
    /// <param name="s1">first sequence</param>
    /// <param name="s2">second sequence</param>
    /// <returns>true if the sequences overlap in the finite range</returns>
    public static bool Overlaps(Sequence s1, Sequence s2) {
        if (s1.End < s2.Start || s2.End < s1.Start)
            return false;
        
        var (gcd, x0, y0) = ExtendedGcd(s1.Interval, -s2.Interval);
        
        if ((s2.Start - s1.Start) % gcd != 0) return false; 
        
        var k = (s2.Start - s1.Start) / gcd;
        x0 *= k;
        y0 *= k;

        var s = s2.Start / gcd;
        var r = s1.Start / gcd;
        
        var aInterval = (Math.Min(s1.End, s2.End) - s1.Start) / s1.Interval;
        var bInterval = (s2.Start - Math.Min(s2.End, s1.End)) / s2.Interval;
        var ta = s > 0 ? (aInterval - x0) / s : (aInterval - x0 + s - 1) / s;
        var tb = r < 0 ? (y0 - bInterval) / r : (y0 - bInterval + r - 1) / r;
        if (s * r > 0) 
            return s > 0 ? tb <= ta : tb >= ta;

        return true;
    }
}