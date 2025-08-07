using Dd.Domain.Reservation.Utils;

namespace Dd.Domain.Test.Reservation.Utils;

public class ScheduleMathTests {
    [Theory]
    [InlineData(99, 13, 1)]
    [InlineData(54, 24, 6)]
    [InlineData(48, 18, 6)]
    [InlineData(101, 10, 1)]
    [InlineData(0, 5, 5)]
    [InlineData(5, 0, 5)]
    [InlineData(0, 0, 0)]
    public void Gcd_ReturnsExpectedResult(int a, int b, int expectedGcd) {
        var result = ScheduleMath.Gcd(a, b);
        Assert.Equal(expectedGcd, result);
    }

    [Theory]
    // add more test parameters here
    [InlineData(99, 13, 1)]
    [InlineData(54, 24, 6)]
    [InlineData(48, 18, 6)]
    [InlineData(30, 20, 10)]
    [InlineData(35, 15, 5)]
    [InlineData(101, 10, 1)]
    [InlineData(17, 19, 1)]
    [InlineData(100, 25, 25)]
    [InlineData(0, 100, 100)]
    [InlineData(123456, 789012, 12)]
    [InlineData(123456789, 987654321, 9)]
    public void ExtendedGcd_ReturnsExpectedGcd(int a, int b, int expectedGcd) {
        var (gcd, x, y) = ScheduleMath.ExtendedGcd(a, b);
        Assert.Equal(expectedGcd, gcd);
        // Check Bézout's identity: a*x + b*y == gcd
        Assert.Equal(expectedGcd, a * x + b * y);
    }

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
}