using Dd.Domain.Reservation.Utils;

namespace Dd.Domain.Test.Reservation.Utils;

public class SequenceTests {
    
    // constructor tests
    [Fact]
    public void Constructor_ShouldSetEndToBeAValidSequenceMember_WhenEndIsNotAValidMember() {
        var sequence = new FiniteSequence(0, 11, 3);
        Assert.Equal(9, sequence.End);
        
        var sequence2 = new FiniteSequence(3, 10, 3);
        Assert.Equal(9, sequence2.End);
    }

    [Fact]
    public void Constructor_ShouldNotAffectAnAlreadyValidEnd_WhenEndIsValidSequenceMember() {
        var sequence = new FiniteSequence(0, 12, 3);
        Assert.Equal(12, sequence.End);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenStartIsGreaterThanOrEqualToEnd() {
        Assert.Throws<ArgumentException>(() => new FiniteSequence(10, 5, 2));
        Assert.Throws<ArgumentException>(() => new FiniteSequence(10, 10, 2));
        Assert.Throws<ArgumentException>(() => new FiniteSequence(10, 15, 6));
    }
}