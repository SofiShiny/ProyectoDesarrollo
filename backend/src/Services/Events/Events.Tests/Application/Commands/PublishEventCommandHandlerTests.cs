/*using Events.Application.Commands;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Domain.Repositories;
using Events.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using Xunit;

namespace Events.Tests.Application.Commands;

public class PublishEventCommandHandlerTests
{
    private readonly Mock<IEventRepository> _eventRepositoryMock;
    private readonly PublishEventCommandHandler _handler;

    public PublishEventCommandHandlerTests()
    {
        _eventRepositoryMock = new Mock<IEventRepository>();
        _handler = new PublishEventCommandHandler(_eventRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidEventId_ShouldPublishEvent()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var command = new PublishEventCommand(eventId);
        
        var startDate = DateTime.UtcNow.AddDays(30);
        var endDate = startDate.AddDays(2);
        var location = new Location("Venue", "Address", "City", "CA", "12345", "USA");
        var eventEntity = new Event("Tech Conference", "Description", location, startDate, endDate, 500, "organizer-001");
        typeof(Event).GetProperty("Id")!.SetValue(eventEntity, eventId);

        _eventRepositoryMock
            .Setup(x => x.GetByIdAsync(eventId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(eventEntity);

        _eventRepositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<Event>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        eventEntity.Status.Should().Be(EventStatus.Published);
        _eventRepositoryMock.Verify(
            x => x.UpdateAsync(It.Is<Event>(e => e.Status == EventStatus.Published), It.IsAny<CancellationToken>()), 
            Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistentEvent_ShouldThrowException()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var command = new PublishEventCommand(eventId);

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