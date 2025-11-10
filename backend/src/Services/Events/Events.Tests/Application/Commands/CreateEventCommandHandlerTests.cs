using Events.Application.Commands;
using Events.Application.DTOs;
using Events.Domain.Entities;
using Events.Domain.Repositories;
using Events.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using Xunit;

namespace Events.Tests.Application.Commands;

public class CreateEventCommandHandlerTests
{
    private readonly Mock<IEventRepository> _eventRepositoryMock;
    private readonly CreateEventCommandHandler _handler;

    public CreateEventCommandHandlerTests()
    {
        _eventRepositoryMock = new Mock<IEventRepository>();
        _handler = new CreateEventCommandHandler(_eventRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldCreateEventAndReturnSuccess()
    {
        // Arrange
        var locationDto = new LocationDto
        {
            VenueName = "Main Hall",
            Address = "123 St",
            City = "NY",
            State = "NY",
            ZipCode = "10001",
            Country = "USA"
        }!; // ✅ operador de supresión aplicado al objeto completo

        var command = new CreateEventCommand(
            Title: "TechConf",
            Description: "Tech conference",
            Location: locationDto,
            StartDate: DateTime.UtcNow.AddDays(5),
            EndDate: DateTime.UtcNow.AddDays(6),
            MaxAttendees: 100,
            OrganizerId: "org-001"
        );

        var expectedEvent = new Event(
            command.Title,
            command.Description,
            new Location(
                locationDto.VenueName,
                locationDto.Address,
                locationDto.City,
                locationDto.State,
                locationDto.ZipCode,
                locationDto.Country
            ),
            command.StartDate,
            command.EndDate,
            command.MaxAttendees,
            command.OrganizerId
        );

        _eventRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Event>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(expectedEvent));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Title.Should().Be(command.Title);
        result.Value.Description.Should().Be(command.Description);
        result.Value.Location.Should().NotBeNull();
        result.Value.Location.City.Should().Be("NY");

        _eventRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Event>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNullLocation_ShouldReturnFailure()
    {
        // Arrange
        var command = new CreateEventCommand(
            Title: "TechConf",
            Description: "Tech conference",
            Location: null!,
            StartDate: DateTime.UtcNow.AddDays(5),
            EndDate: DateTime.UtcNow.AddDays(6),
            MaxAttendees: 100,
            OrganizerId: "org-001"
        );

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("Location is required");
        _eventRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Event>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenRepositoryThrowsArgumentException_ShouldReturnFailure()
    {
        // Arrange
        var locationDto = new LocationDto
        {
            VenueName = "Main Hall",
            Address = "123 St",
            City = "NY",
            State = "NY",
            ZipCode = "10001",
            Country = "USA"
        }!; // ✅ operador de supresión aplicado al objeto completo

        var command = new CreateEventCommand(
            Title: "TechConf",
            Description: "Tech conference",
            Location: locationDto,
            StartDate: DateTime.UtcNow.AddDays(5),
            EndDate: DateTime.UtcNow.AddDays(6),
            MaxAttendees: 100,
            OrganizerId: "org-001"
        );

        _eventRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Event>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new ArgumentException("Start date must be in the future (Parameter 'startDate')"));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("Start date must be in the future (Parameter 'startDate')");
    }
}