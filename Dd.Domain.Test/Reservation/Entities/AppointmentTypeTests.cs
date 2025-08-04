using Dd.Domain.Reservation.Entities;

namespace Dd.Domain.Test.Reservation.Entities;

public class AppointmentTypeTests {
    // constructor tests
    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenNameIsNull() {
        // Arrange
        string? name = null;
        const string description = "Test Description";
        const int ticks = 10;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new AppointmentType(name!, description, ticks));
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenDescriptionIsNull() {
        // Arrange
        const string name = "Test Appointment Type";
        string? description = null;
        const int ticks = 10;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new AppointmentType(name, description!, ticks));
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentOutOfRangeException_WhenTicksIsZeroOrNegative() {
        // Arrange
        const string name = "Test Appointment Type";
        const string description = "Test Description";
        const int ticks = 0; // or negative value

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new AppointmentType(name, description, ticks));
    }

    [Fact]
    public void Constructor_ShouldInitializeProperties_WhenValidParametersAreProvided() {
        // Arrange
        const string name = "Test Appointment Type";
        const string description = "Test Description";
        const int ticks = 10;

        // Act
        var appointmentType = new AppointmentType(name, description, ticks);

        // Assert
        Assert.Equal(name, appointmentType.Name);
        Assert.Equal(description, appointmentType.Description);
        Assert.Equal(ticks, appointmentType.Ticks);
    }
}