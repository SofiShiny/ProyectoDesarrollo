/*using Events.Application.Commands;
using Events.Domain.Entities;
using Events.Domain.Repositories;
using Events.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using Xunit;

namespace Events.Tests.Application.Commands;

public class RegisterAttendeeCommandHandlerTests
{
    private readonly Mock<IEventRepository> _eventRepositoryMock;
    private readonly RegisterAttendeeCommandHandler _handler;

    public RegisterAttendeeCommandHandlerTests()
    {
        _eventRepositoryMock = new Mock<IEventRepository>();
        _handler = new RegisterAttendeeCommandHandler(_eventRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldRegisterAttendee()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var userId = "user-001"; // Changed from Guid to string to match RegisterAttendeeCommand signature
        var userName = "John Doe";
        var email = "user@example.com";
        var command = new RegisterAttendeeCommand(eventId, userId, userName, email);
        
        var startDate = DateTime.UtcNow.AddDays(30);
        var endDate = startDate.AddDays(2);
        var location = new Location("Venue", "Address", "City", "CA", "12345", "USA");
        var eventEntity = new Event("Tech Conference", "Description", location, startDate, endDate, 500, "organizer-001");
        
        var idProperty = typeof(Event).GetProperty("Id");
        if (idProperty != null && idProperty.CanWrite)
        {
            idProperty.SetValue(eventEntity, eventId);
        }
        
        eventEntity.Publish();

        _eventRepositoryMock
            .Setup(x => x.GetByIdAsync(eventId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(eventEntity);

        _eventRepositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<Event>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.UserId.Should().Be(userId);
        result.Email.Should().Be(email);
        eventEntity.Attendees.Should().ContainSingle();
        _eventRepositoryMock.Verify(
            x => x.UpdateAsync(It.IsAny<Event>(), It.IsAny<CancellationToken>()), 
            Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistentEvent_ShouldThrowException()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var command = new RegisterAttendeeCommand(eventId, "user-001", "John Doe", "user@example.com");

        _eventRepositoryMock
            .Setup(x => x.GetByIdAsync(eventId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Event?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Event with ID {eventId} not found");
    }
}
*/