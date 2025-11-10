/*using Events.Application.DTOs;
using FluentAssertions;
using Xunit;

namespace Events.Tests.Application.DTOs;

public class AttendeeDtoTests
{
    [Fact]
    public void AttendeeDto_ShouldInitializeAllProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "John Doe";
        var email = "john@example.com";
        var registeredAt = DateTime.UtcNow;

        // Act
        var dto = new AttendeeDto
        {
            Id = id,
            Name = name,
            Email = email,
            RegisteredAt = registeredAt
        };

        // Assert
        dto.Id.Should().Be(id);
        dto.Name.Should().Be(name);
        dto.Email.Should().Be(email);
        dto.RegisteredAt.Should().Be(registeredAt);
    }

    [Fact]
    public void AttendeeDto_ShouldAllowNullValues()
    {
        // Arrange & Act
        var dto = new AttendeeDto
        {
            Name = null,
            Email = null
        };

        // Assert
        dto.Name.Should().BeNull();
        dto.Email.Should().BeNull();
    }
}
*/