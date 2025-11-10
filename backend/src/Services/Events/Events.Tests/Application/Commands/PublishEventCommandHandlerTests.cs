/*using Events.Application.Comandos;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Domain.Repositories;
using Events.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using Xunit;

namespace Events.Tests.Application.Comandos;

public class PublishEventComandoHandlerTests
{
    private readonly Mock<IEventRepository> _eventRepositoryMock;
    private readonly PublishEventComandoHandler _handler;

    public PublishEventComandoHandlerTests()
    {
        _eventRepositoryMock = new Mock<IEventRepository>();
        _handler = new PublishEventComandoHandler(_eventRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidEventId_ShouldPublishEvent()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var comando = new PublishEventComando(eventId);
        
        var startDate = DateTime.UtcNow.AddDays(30);
        var endDate = startDate.AddDays(2);
        var direccion = new Location("Venue", "Direccion", "Ciudad", "CA", "12345", "USA");
        var eventEntity = new Event("Tech Conference", "Description", direccion, startDate, endDate, 500, "organizer-001");
        typeof(Event).GetProperty("Id")!.SetValue(eventEntity, eventId);

        _eventRepositoryMock
            .Setup(x => x.GetByIdAsync(eventId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(eventEntity);

        _eventRepositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<Event>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(comando, CancellationToken.None);

        // Assert
        eventEntity.Status.Should().Be(EventStatus.Publicado);
        _eventRepositoryMock.Verify(
            x => x.UpdateAsync(It.Is<Event>(e => e.Status == EventStatus.Publicado), It.IsAny<CancellationToken>()), 
            Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistentEvent_ShouldThrowException()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var comando = new PublishEventComando(eventId);

        _eventRepositoryMock
            .Setup(x => x.GetByIdAsync(eventId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Event?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(comando, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Event with ID {eventId} not found");
    }
}
*/