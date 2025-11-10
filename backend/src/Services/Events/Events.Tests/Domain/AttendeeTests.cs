/*using Events.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Events.Tests.Domain;

public class AttendeeTests
{
    [Fact]
    public void CreateAttendee_WithValidData_ShouldSucceed()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var userId = "user-123";
        var userName = "John Doe";
        var email = "user@example.com";

        // Act
        var attendee = new Attendee(eventId, userId, userName, email);

        // Assert
        attendee.Should().NotBeNull();
        attendee.EventId.Should().Be(eventId);
        attendee.UserId.Should().Be(userId);
        attendee.UserName.Should().Be(userName);
        attendee.Email.Should().Be(email);
        attendee.RegisteredAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void CreateAttendee_WithEmptyEventId_ShouldThrowException()
    {
        // Arrange
        var userId = "user-123";
        var userName = "John Doe";
        var email = "user@example.com";

        // Act
        Action act = () => new Attendee(Guid.Empty, userId, userName, email);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("*EventId*");
    }

    [Fact]
    public void CreateAttendee_WithEmptyUserId_ShouldThrowException()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var userName = "John Doe";
        var email = "user@example.com";

        // Act
        Action act = () => new Attendee(eventId, string.Empty, userName, email);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("*UserId*");
    }

    [Fact]
    public void CreateAttendee_WithInvalidEmail_ShouldThrowException()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var userId = "user-123";
        var userName = "John Doe";

        // Act
        Action act = () => new Attendee(eventId, userId, userName, "invalid-email");

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("*email*");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void CreateAttendee_WithNullOrEmptyEmail_ShouldThrowException(string email)
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var userId = "user-123";
        var userName = "John Doe";

        // Act
        Action act = () => new Attendee(eventId, userId, userName, email);

        // Assert
        act.Should().Throw<ArgumentException>();
    }
}
*/