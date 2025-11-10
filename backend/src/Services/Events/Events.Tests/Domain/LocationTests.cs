/*using Events.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Events.Tests.Domain;

public class LocationTests
{
    [Fact]
    public void CreateLocation_WithValidData_ShouldSucceed()
    {
        // Arrange
        var venueName = "Convention Center";
        var address = "123 Main St";
        var city = "New York";
        var state = "NY";
        var postalCode = "10001";
        var country = "USA";

        // Act
        var location = new Location(venueName, address, city, state, postalCode, country);

        // Assert
        location.Should().NotBeNull();
        location.VenueName.Should().Be(venueName);
        location.Address.Should().Be(address);
        location.City.Should().Be(city);
        location.State.Should().Be(state);
        location.PostalCode.Should().Be(postalCode);
        location.Country.Should().Be(country);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateLocation_WithInvalidVenueName_ShouldThrowException(string venueName)
    {
        // Act
        Action act = () => new Location(venueName, "Address", "City", "State", "12345", "Country");

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("*VenueName*");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateLocation_WithInvalidCity_ShouldThrowException(string city)
    {
        // Act
        Action act = () => new Location("Venue", "Address", city, "State", "12345", "Country");

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("*City*");
    }

    [Fact]
    public void Equals_WithSameValues_ShouldReturnTrue()
    {
        // Arrange
        var location1 = new Location("Venue", "123 Main St", "New York", "NY", "10001", "USA");
        var location2 = new Location("Venue", "123 Main St", "New York", "NY", "10001", "USA");

        // Act
        var areEqual = location1.Equals(location2);

        // Assert
        areEqual.Should().BeTrue();
    }

    [Fact]
    public void Equals_WithDifferentValues_ShouldReturnFalse()
    {
        // Arrange
        var location1 = new Location("Venue 1", "123 Main St", "New York", "NY", "10001", "USA");
        var location2 = new Location("Venue 2", "456 Oak Ave", "Boston", "MA", "02101", "USA");

        // Act
        var areEqual = location1.Equals(location2);

        // Assert
        areEqual.Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_WithSameValues_ShouldReturnSameHashCode()
    {
        // Arrange
        var location1 = new Location("Venue", "123 Main St", "New York", "NY", "10001", "USA");
        var location2 = new Location("Venue", "123 Main St", "New York", "NY", "10001", "USA");

        // Act
        var hash1 = location1.GetHashCode();
        var hash2 = location2.GetHashCode();

        // Assert
        hash1.Should().Be(hash2);
    }
}
*/