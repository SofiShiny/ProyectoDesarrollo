/*using Events.Application.Comandos;
using Events.Domain.Entities;
using Events.Domain.Repositories;
using Events.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using Xunit;

namespace Events.Tests.Application.Comandos;

public class RegisterAttendeeComandoHandlerTests
{
    private readonly Mock<IEventRepository> _eventRepositoryMock;
    private readonly RegisterAttendeeComandoHandler _handler;

    public RegisterAttendeeComandoHandlerTests()
    {
        _eventRepositoryMock = new Mock<IEventRepository>();
        _handler = new RegisterAttendeeComandoHandler(_eventRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidComando_ShouldRegisterAttendee()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var userId = "user-001"; // Changed from Guid to string to match RegisterAttendeeComando signature
        var userName = "John Doe";
        var email = "user@example.com";
        var comando = new RegisterAttendeeComando(eventId, userId, userName, email);
        
        var startDate = DateTime.UtcNow.AddDays(30);
        var endDate = startDate.AddDays(2);
        var direccion = new Location("Venue", "Direccion", "Ciudad", "CA", "12345", "USA");
        var eventEntity = new Event("Tech Conference", "Description", direccion, startDate, endDate, 500, "organizer-001");
        
        var idProperty = typeof(Event).GetProperty("Id");
        if (idProperty != null && idProperty.CanWrite)
        {
            idProperty.SetValue(eventEntity, eventId);
        }
        
        eventEntity.Publicar();

        _eventRepositoryMock
            .Setup(x => x.GetByIdAsync(eventId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(eventEntity);

        _eventRepositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<Event>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(comando, CancellationToken.None);

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
        var comando = new RegisterAttendeeComando(eventId, "user-001", "John Doe", "user@example.com");

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