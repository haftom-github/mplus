using Dd.Domain.Reservation.Utils;

namespace Dd.Domain.Test.Reservation.Utils;

public class ScheduleMathTests {
    [Fact]
    public void Overlaps_FiniteSequences_ShouldReturnFalseWhenEffectiveRangesDoNotOverlap() {
        var s1 = new FiniteSequence(3, 20, 1);
        var s2 = new FiniteSequence(21, 40, 1);
        
        var result = ScheduleMath.Overlaps(s1, s2);
        Assert.False(result);

        s1 = new FiniteSequence(0, 69, 10);
        s2 = new FiniteSequence(65, 80, 5);
        
        result = ScheduleMath.Overlaps(s1, s2);
        Assert.False(result);
        
        
    }
    
    [Fact]
    public void Overlaps_FiniteSequences_ShouldReturnFalse_WhenSequencesNeverOverlap_NotConsideringWhereTheSequenceEnds() {
        var s1 = new FiniteSequence(0, 900, 2);
        var s2 = new FiniteSequence(1, 5478, 2);
        
        var result = ScheduleMath.Overlaps(s1, s2);
        Assert.False(result);

        s1 = new FiniteSequence(0, 754893, 4);
        s2 = new FiniteSequence(1, 2457935, 6);
        
        result = ScheduleMath.Overlaps(s1, s2);
        Assert.False(result);
        
        s1 = new FiniteSequence(30, 60, 5);
        s2 = new FiniteSequence(31, 37, 6);
        result = ScheduleMath.Overlaps(s1, s2);
        Assert.False(result);
        
        // var s1Inf = new Sequence(30, 5);
        // var s2Inf = new Sequence(31, 6);
        // result = ScheduleMath.Overlaps(s1Inf, s2Inf);
        // Assert.False(result);
    }
    
    // tests for non-finite sequences
    [Theory]
    [InlineData(15, 3, 20, 1)] 
    [InlineData(10, 5, 30, 2)] 
    [InlineData(0, 10, 100, 5)]
    [InlineData(5, 0, 50, 10)]
    public void Overlaps_FirstSequenceNotFinite_ShouldFallbackToFiniteSequenceVersionBasedOnSecondOne(int s1Start, int s2Start, int s2End, int step) {
        var s1 = new Sequence(s1Start, step);
        var s2 = new FiniteSequence(s2Start, s2End, step);
        var s1Fallback = new FiniteSequence(s1Start, s2.End, step);

        Assert.Equal(ScheduleMath.Overlaps(s1, s2), ScheduleMath.Overlaps(s1Fallback, s2));
    }
    
    [Fact]
    public void Overlaps_FirstSequenceNotFinite_ShouldThrowExceptionWhenTheFiniteEndIsLessThanTheInfiniteStart() {
        var s1 = new Sequence(10, 5);
        var s2 = new FiniteSequence(5, 15, 3);
        
        Assert.Throws<ArgumentException>(() => ScheduleMath.Overlaps(s1, s2));
    }
    
    // tests for OverlapOfRange

    [Theory]
    [InlineData(15, 3, 20, 1, null, null)]
    [InlineData(0, 15, 5, 20, 5, 15)]
    [InlineData(-5, 6, -2, 5, -2, 5)]
    [InlineData(10, 20, 15, 25, 15, 20)]
    [InlineData(10, 20, 25, 30, null, null)]
    [InlineData(10, 20, 20, 30, 20, 20)]
    [InlineData(10, 20, 10, 30, 10, 20)]
    [InlineData(10, 20, 10, 20, 10, 20)]
    [InlineData(10, 20, 5, 15, 10, 15)]
    [InlineData(7, 3, 4, 5, null, null)]
    [InlineData(3, 7, 5, 4, null, null)]
    public void OverlapsOfRange(int al, int au, int bl, int bu, int? exl, int? exu) {
        var bounds = ScheduleMath.OverlapOfRange(al, au, bl, bu);
        if (exl == null || exu == null) {
            Assert.Null(bounds);
        }
        else {
            Assert.NotNull(bounds);
            Assert.Equal(exl, bounds.Value.l);
            Assert.Equal(exu, bounds.Value.u);
        }
    }
    
    [Fact]
    public void FirstOverlap_ShouldReturnFirstOverlapOfTwoFiniteSequences() {
        var s1 = new FiniteSequence(0, 20, 5);
        var s2 = new FiniteSequence(10, 30, 5);
        
        var firstOverlap = ScheduleMath.FirstOverlap(s1, s2);
        Assert.Equal(10, firstOverlap.f);
        
        s1 = new FiniteSequence(0, 20, 5);
        s2 = new FiniteSequence(21, 30, 5);
        firstOverlap = ScheduleMath.FirstOverlap(s1, s2);
        Assert.Equal(0, firstOverlap.count);
        
        s1 = new FiniteSequence(0, 20, 5);
        s2 = new FiniteSequence(5, 25, 5);
        firstOverlap = ScheduleMath.FirstOverlap(s1, s2);
        Assert.Equal(5, firstOverlap.f);
        
        s1 = new FiniteSequence(0, 20, 5);
        s2 = new FiniteSequence(0, 20, 5);
        firstOverlap = ScheduleMath.FirstOverlap(s1, s2);
        Assert.Equal(0, firstOverlap.f);
        
        s1 = new FiniteSequence(0, 25, 5);
        s2 = new FiniteSequence(1, 26, 6);
        firstOverlap = ScheduleMath.FirstOverlap(s1, s2);
        Assert.Equal(25, firstOverlap.f);
    }

    [Fact]
    public void CeilDiv() {
        const int testCount = 100_000;

        for (var i = 0; i < testCount; i++) {
            var a = Rng.Next(int.MinValue / 2, int.MaxValue / 2);

            int b;
            do {
                b = Rng.Next(int.MinValue / 2, int.MaxValue / 2);
            } while (b == 0); // avoid division by zero

            var expected = (int)Math.Ceiling((double)a / b);
            var actual = ScheduleMath.Ceil(a, b);

            Assert.Equal(expected, actual);
        }
    }
    
    private static readonly Random Rng = new Random();
    [Fact]
    public void FloorDiv() {
        const int testCount = 100_000;

        for (var i = 0; i < testCount; i++) {
            var a = Rng.Next(int.MinValue / 2, int.MaxValue / 2);
            
            int b;
            do
            {
                b = Rng.Next(int.MinValue / 2, int.MaxValue / 2);
            } while (b == 0); // avoid division by zero

            var expected = (int)Math.Floor((double)a / b);
            var actual = ScheduleMath.Floor(a, b);

            Assert.Equal(expected, actual);
        }
    }
    
    // Brute force helper to verify correctness
    private static int? BruteForceFirstOverlap(Sequence s1, Sequence s2, int searchLimit = 100000)
    {
        int? earliest = null;
        for (var i = 0; i < searchLimit; i++)
        {
            var val1 = s1.Start + i * s1.Interval;
            if (val1 < s1.Start || val1 < s2.Start) continue; // ensure "future"
            for (var j = 0; j < searchLimit; j++)
            {
                var val2 = s2.Start + j * s2.Interval;
                if (val1 != val2) continue;
                earliest = val1;
                break;
            }
            if (earliest.HasValue) break;
        }
        return earliest;
    }

    [Fact]
    public void OverlapInFuture_PositiveIntervals()
    {
        var s1 = new Sequence(5, 3);  // 5, 8, 11, 14...
        var s2 = new Sequence(2, 4);  // 2, 6, 10, 14...
        var overlap = ScheduleMath.FirstOverlap(s1, s2);
        Assert.Equal(14, overlap.f);
    }

    [Fact]
    public void OverlapAtStart()
    {
        var s1 = new Sequence(10, 5);
        var s2 = new Sequence(10, 7);
        var overlap = ScheduleMath.FirstOverlap(s1, s2);
        Assert.Equal(10, overlap.f);
    }

    [Fact]
    public void NoOverlap()
    {
        var s1 = new Sequence(0, 4);
        var s2 = new Sequence(3, 6);
        Assert.Equal(0, ScheduleMath.FirstOverlap(s1, s2).count);
    }

    [Theory]
    [InlineData(5, 3, 2, 4)]
    [InlineData(0, 4, 3, 6)]
    [InlineData(100, 12, 110, 18)]
    [InlineData(15, 5, 40, 10)]
    public void MatchesBruteForce(int start1, int int1, int start2, int int2)
    {
        var s1 = new Sequence(start1, int1);
        var s2 = new Sequence(start2, int2);

        var expected = BruteForceFirstOverlap(s1, s2);
        var actual = ScheduleMath.FirstOverlap(s1, s2).f;

        Assert.Equal(expected, actual);
    }
}