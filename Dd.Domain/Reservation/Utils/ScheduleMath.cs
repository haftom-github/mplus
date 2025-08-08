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
    /// <param name="s1">first sequence</param>
    /// <param name="s2">the second sequence</param>
    /// <returns>true if the sequences have a possibility of overlapping</returns>
    public static bool Overlaps(Sequence s1, Sequence s2) {
        if (s1 is not FiniteSequence && s2 is not FiniteSequence)
            return (s1.Start - s2.Start) % Gcd(s1.Interval, s2.Interval) == 0;
        var end = ((FiniteSequence)s2).End;
        var finiteS1 = s1 as FiniteSequence ?? new FiniteSequence(s1.Start, ((FiniteSequence)s2).End, s1.Interval);
        var finiteS2 = s2 as FiniteSequence ?? new FiniteSequence(s2.Start, finiteS1.End, s2.Interval);
        if (finiteS1.End < finiteS2.Start || finiteS2.End < finiteS1.Start)
            return false;
        
        return OverlapsFinite(finiteS1, finiteS2);
    }
    

    /// <summary>
    /// checks if two finite sequences meet.
    /// </summary>
    /// <param name="s1">first sequence</param>
    /// <param name="s2">second sequence</param>
    /// <returns>true if the sequences overlap in the finite range</returns>
    private static bool OverlapsFinite(FiniteSequence s1, FiniteSequence s2) {
        var (gcd, x0, y0) = ExtendedGcd(s1.Interval, -s2.Interval);
        
        if ((s2.Start - s1.Start) % gcd != 0) return false; 
        
        var k = (s2.Start - s1.Start) / gcd;
        x0 *= k;
        y0 *= k;

        var xStep = -s2.Interval / gcd;
        var yStep = s1.Interval / gcd;
        
        var minEnd = Math.Min(s1.End, s2.End);
        
        var aOccurence = (minEnd - s1.Start) / s1.Interval;
        var bOccurence = (s2.Start - minEnd) / s2.Interval;
        var ta = xStep > 0 ? (aOccurence - x0) / xStep : (aOccurence - x0 + xStep - 1) / xStep;
        var tb = yStep < 0 ? (y0 - bOccurence) / yStep : (y0 - bOccurence + yStep - 1) / yStep;
        if (xStep * yStep > 0) 
            return xStep > 0 ? tb <= ta : tb >= ta;

        return true;
    }

    public static int? FirstOverlapFinite(FiniteSequence s1, FiniteSequence s2) {
        var (gcd, x0, y0) = ExtendedGcd(s1.Interval, -s2.Interval);
        
        if ((s2.Start - s1.Start) % gcd != 0) return null; 
        
        var k = (s2.Start - s1.Start) / gcd;
        x0 *= k;
        y0 *= k;
    
        var xStep = -s2.Interval / gcd;
        var yStep = s1.Interval / gcd;
        
        var minEnd = Math.Min(s1.End, s2.End);
        
        var aOccurence = (minEnd - s1.Start) / s1.Interval;
        var bOccurence = (s2.Start - minEnd) / -s2.Interval;
        var ta = xStep > 0 ? Floor(aOccurence - x0, xStep) : Ceil(aOccurence - x0, xStep);
        var tb = yStep < 0 ? Floor(y0 - bOccurence, yStep) : Ceil(y0 - bOccurence, yStep);
        var ra = xStep > 0 ? Ceil(-x0, xStep) : Floor(-x0, xStep);
        var rb = yStep < 0 ? Ceil(y0, yStep) : Floor(y0, yStep);
        int? t;
        if ((xStep ^ yStep) >= 0) {
            t = xStep > 0
                ? OverlapOfRange(ra, ta, tb, rb)?.l
                : OverlapOfRange(ta, ra, rb, tb)?.u;
            if (t == null) return null;
            return s1.S(x0 + t.Value * xStep);
        }

        t = xStep > 0
            ? OverlapOfRange(ra, ta, rb, tb)?.l
            : OverlapOfRange(ta, ra, tb, rb)?.u;
        if (t == null) return null;
        return s1.S(x0 + t.Value * xStep);
    }

    public static (int l, int u)? OverlapOfRange(int al, int au, int bl, int bu) {
        var minUpper = Math.Min(au, bu);
        var maxLower = Math.Max(al, bl);
        if (minUpper < maxLower) return null;
        return (maxLower, minUpper);
    }
    
    public static int Floor(int a, int b) {
        return (a ^ b) < 0 ? a < 0 ? (a - b + 1) / b : (a - b -1) / b : a / b;
    }
    
    public static int Ceil(int a, int b) {
        return (a ^ b) < 0 ? a / b : a < 0 ? (a + b + 1) / b : (a + b - 1) / b;
    }
}