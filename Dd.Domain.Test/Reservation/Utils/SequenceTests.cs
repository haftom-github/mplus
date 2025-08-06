using Dd.Domain.Reservation.Utils;

namespace Dd.Domain.Test.Reservation.Utils;

public class SequenceTests {
    
    // constructor tests
    [Fact]
    public void Constructor_ShouldSetEndToBeAValidSequenceMember_WhenEndIsNotAValidMember() {
        var sequence = new Sequence(0, 11, 3);
        Assert.Equal(9, sequence.End);
    }

    [Fact]
    public void Constructor_ShouldNotAffectAnAlreadyValidEnd_WhenEndIsValidSequenceMember() {
        var sequence = new Sequence(0, 12, 3);
        Assert.Equal(12, sequence.End);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenStartIsGreaterThanEnd() {
        Assert.Throws<ArgumentException>(() => new Sequence(10, 5, 2));
        Assert.Throws<ArgumentException>(() => new Sequence(10, 15, 4));
    }
}