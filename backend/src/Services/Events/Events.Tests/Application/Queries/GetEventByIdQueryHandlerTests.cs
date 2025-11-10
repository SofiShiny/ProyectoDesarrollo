/*using Events.Application.Queries;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Domain.Repositories;
using Events.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using Xunit;

namespace Events.Tests.Application.Queries;

public class GetEventByIdQueryHandlerTests
{
    private readonly Mock<IEventRepository> _repositoryMock;
    private readonly GetEventByIdQueryHandler _handler;

    public GetEventByIdQueryHandlerTests()
    {
        _repositoryMock = new Mock<IEventRepository>();
        _handler = new GetEventByIdQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnEventDto_WhenEventExists()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var location = new Location("Conference Center", "123 Main St", "Tech City", "CA", "94000", "USA");
        var startDate = DateTime.UtcNow.AddMonths(1);
        var endDate = startDate.AddHours(8);
        var @event = new Event(
            "Tech Conference 2024",
            "Annual tech conference",
            location,
            startDate,
            endDate,
            100,
            "organizer-001");

        typeof(Event).GetProperty("Id")!.SetValue(@event, eventId);

        _repositoryMock.Setup(x => x.GetByIdAsync(eventId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(@event);

        var query = new GetEventByIdQuery(eventId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(@event.Id);
        result.Title.Should().Be("Tech Conference 2024");
        result.Description.Should().Be("Annual tech conference");
        result.Status.Should().Be(EventStatus.Draft.ToString());
        
        _repositoryMock.Verify(x => x.GetByIdAsync(eventId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenEventDoesNotExist()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        _repositoryMock.Setup(x => x.GetByIdAsync(eventId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Event?)null);

        var query = new GetEventByIdQuery(eventId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeNull();
        _repositoryMock.Verify(x => x.GetByIdAsync(eventId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldMapLocationCorrectly_WhenEventHasLocation()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var location = new Location("Venue Name", "Address Line", "City", "CA", "90000", "USA");
        var startDate = DateTime.UtcNow.AddDays(30);
        var endDate = startDate.AddHours(4);
        var @event = new Event(
            "Conference",
            "Description",
            location,
            startDate,
            endDate,
            50,
            "organizer-001");

        typeof(Event).GetProperty("Id")!.SetValue(@event, eventId);

        _repositoryMock.Setup(x => x.GetByIdAsync(eventId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(@event);

        var query = new GetEventByIdQuery(eventId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Location.Should().NotBeNull();
        result.Location.VenueName.Should().Be("Venue Name");
        result.Location.Address.Should().Be("Address Line");
        result.Location.City.Should().Be("City");
        result.Location.State.Should().Be("CA");
        result.Location.ZipCode.Should().Be("90000");
        result.Location.Country.Should().Be("USA");
    }

    [Fact]
    public async Task Handle_ShouldHandleCancellation_WhenTokenIsCancelled()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var cts = new CancellationTokenSource();
        cts.Cancel();

        _repositoryMock.Setup(x => x.GetByIdAsync(eventId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new OperationCanceledException());

        var query = new GetEventByIdQuery(eventId);

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(() => 
            _handler.Handle(query, cts.Token));
    }

    [Fact]
    public async Task Handle_ShouldIncludeAttendeesCount_WhenEventHasAttendees()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var location = new Location("Venue", "Address", "City", "CA", "12345", "USA");
        var startDate = DateTime.UtcNow.AddDays(10);
        var endDate = startDate.AddHours(2);
        var @event = new Event("Event", "Desc", location, startDate, endDate, 100, "organizer-001");

        typeof(Event).GetProperty("Id")!.SetValue(@event, eventId);
        @event.Publish();

        // Register attendees
        @event.RegisterAttendee("user-001", "John Doe", "john@example.com");
        @event.RegisterAttendee("user-002", "Jane Smith", "jane@example.com");

        _repositoryMock.Setup(x => x.GetByIdAsync(eventId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(@event);

        var query = new GetEventByIdQuery(eventId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Attendees.Should().HaveCount(2);
        result.Attendees.Should().Contain(a => a.UserName == "John Doe");
        result.Attendees.Should().Contain(a => a.UserName == "Jane Smith");
    }
}
*/