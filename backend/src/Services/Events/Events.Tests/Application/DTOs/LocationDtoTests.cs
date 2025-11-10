/*using Events.Application.DTOs;
using FluentAssertions;
using Xunit;

namespace Events.Tests.Application.DTOs;

public class LocationDtoTests
{
    [Fact]
    public void LocationDto_ShouldInitializeAllProperties()
    {
        // Arrange & Act
        var dto = new LocationDto
        {
            VenueName = "Conference Center",
            Address = "123 Main Street",
            City = "San Francisco",
            State = "CA",
            ZipCode = "94105"
        };

        // Assert
        dto.VenueName.Should().Be("Conference Center");
        dto.Address.Should().Be("123 Main Street");
        dto.City.Should().Be("San Francisco");
        dto.State.Should().Be("CA");
        dto.ZipCode.Should().Be("94105");
    }

    [Fact]
    public void LocationDto_ShouldAllowEmptyStrings()
    {
        // Arrange & Act
        var dto = new LocationDto
        {
            VenueName = string.Empty,
            Address = string.Empty,
            City = string.Empty,
            State = string.Empty,
            ZipCode = string.Empty
        };

        // Assert
        dto.VenueName.Should().BeEmpty();
        dto.Address.Should().BeEmpty();
        dto.City.Should().BeEmpty();
        dto.State.Should().BeEmpty();
        dto.ZipCode.Should().BeEmpty();
    }
}
*/