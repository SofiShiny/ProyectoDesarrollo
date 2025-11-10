/*using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Events.Tests.Domain;

public class EventTests
{
    [Fact]
    public void CreateEvent_WithValidData_ShouldSucceed()
    {
        // Arrange
        var title = "Tech Conference 2025";
        var description = "Annual technology conference";
        var startDate = DateTime.UtcNow.AddDays(30);
        var endDate = startDate.AddDays(2);
        var direccion = new Location("Convention Center", "123 Main St", "New York", "NY", "10001", "USA");
        var maxAttendees = 500;
        var organizerId = "organizer-123";

        // Act
        var eventEntity = new Event(title, description, direccion, startDate, endDate, maxAttendees, organizerId);

        // Assert
        eventEntity.Should().NotBeNull();
        eventEntity.Title.Should().Be(title);
        eventEntity.Description.Should().Be(description);
        eventEntity.StartDate.Should().Be(startDate);
        eventEntity.EndDate.Should().Be(endDate);
        eventEntity.Location.Should().Be(direccion);
        eventEntity.MaxAttendees.Should().Be(maxAttendees);
        eventEntity.Status.Should().Be(EventStatus.Draft);
        eventEntity.Attendees.Should().BeEmpty();
    }

    [Fact]
    public void CreateEvent_WithEndDateBeforeStartDate_ShouldThrowException()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(30);
        var endDate = startDate.AddDays(-1);
        var direccion = new Location("Venue", "Direccion", "Ciudad", "ST", "12345", "USA");

        // Act
        Action act = () => new Event("Title", "Description", direccion, startDate, endDate, 100, "org-123");

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("End date must be after start date*");
    }

    [Fact]
    public void CreateEvent_WithNegativeMaxAttendees_ShouldThrowException()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(30);
        var endDate = startDate.AddDays(2);
        var direccion = new Location("Venue", "Direccion", "Ciudad", "ST", "12345", "USA");

        // Act
        Action act = () => new Event("Title", "Description", direccion, startDate, endDate, -1, "org-123");

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Max attendees must be greater than zero*");
    }

    [Fact]
    public void Publish_DraftEvent_ShouldChangeStatusToPublished()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(30);
        var endDate = startDate.AddDays(2);
        var direccion = new Location("Venue", "Direccion", "Ciudad", "ST", "12345", "USA");
        var eventEntity = new Event("Tech Conference", "Description", direccion, startDate, endDate, 500, "org-123");

        // Act
        eventEntity.Publicar();

        // Assert
        eventEntity.Status.Should().Be(EventStatus.Publicado);
    }

    [Fact]
    public void Publish_AlreadyPublishedEvent_ShouldThrowException()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(30);
        var endDate = startDate.AddDays(2);
        var direccion = new Location("Venue", "Direccion", "Ciudad", "ST", "12345", "USA");
        var eventEntity = new Event("Tech Conference", "Description", direccion, startDate, endDate, 500, "org-123");
        eventEntity.Publicar();

        // Act
        Action act = () => eventEntity.Publicar();

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Cannot publicar event in Publicado status");
    }

    [Fact]
    public void Cancel_PublishedEvent_ShouldChangeStatusToCancelled()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(30);
        var endDate = startDate.AddDays(2);
        var direccion = new Location("Venue", "Direccion", "Ciudad", "ST", "12345", "USA");
        var eventEntity = new Event("Tech Conference", "Description", direccion, startDate, endDate, 500, "org-123");
        eventEntity.Publicar();

        // Act
        eventEntity.Cancel();

        // Assert
        eventEntity.Status.Should().Be(EventStatus.Cancelled);
    }

    [Fact]
    public void RegisterAttendee_WithAvailableCapacity_ShouldSucceed()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(30);
        var endDate = startDate.AddDays(2);
        var direccion = new Location("Venue", "Direccion", "Ciudad", "ST", "12345", "USA");
        var eventEntity = new Event("Tech Conference", "Description", direccion, startDate, endDate, 500, "org-123");
        eventEntity.Publicar();
        var userId = "user-123";
        var userName = "John Doe";
        var email = "user@example.com";

        // Act
        eventEntity.RegisterAttendee(userId, userName, email);

        // Assert
        eventEntity.Attendees.Should().ContainSingle();
        var attendee = eventEntity.Attendees.First();
        attendee.UserId.Should().Be(userId);
        attendee.UserName.Should().Be(userName);
        attendee.Email.Should().Be(email);
    }

    [Fact]
    public void RegisterAttendee_WhenEventNotPublished_ShouldThrowException()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(30);
        var endDate = startDate.AddDays(2);
        var direccion = new Location("Venue", "Direccion", "Ciudad", "ST", "12345", "USA");
        var eventEntity = new Event("Tech Conference", "Description", direccion, startDate, endDate, 500, "org-123");
        var userId = "user-123";

        // Act
        Action act = () => eventEntity.RegisterAttendee(userId, "John Doe", "user@example.com");

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Cannot register for an unpublished event");
    }

    [Fact]
    public void RegisterAttendee_WhenEventFull_ShouldThrowException()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(30);
        var endDate = startDate.AddDays(2);
        var direccion = new Location("Venue", "Direccion", "Ciudad", "ST", "12345", "USA");
        var eventEntity = new Event("Tech Conference", "Description", direccion, startDate, endDate, 1, "org-123");
        eventEntity.Publicar();
        eventEntity.RegisterAttendee("user-1", "User One", "user1@example.com");

        // Act
        Action act = () => eventEntity.RegisterAttendee("user-2", "User Two", "user2@example.com");

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Event is full");
    }

    [Fact]
    public void IsFull_WhenAtMaxCapacity_ShouldReturnTrue()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(30);
        var endDate = startDate.AddDays(2);
        var direccion = new Location("Venue", "Direccion", "Ciudad", "ST", "12345", "USA");
        var eventEntity = new Event("Tech Conference", "Description", direccion, startDate, endDate, 2, "org-123");
        eventEntity.Publicar();
        eventEntity.RegisterAttendee("user-1", "User One", "user1@example.com");
        eventEntity.RegisterAttendee("user-2", "User Two", "user2@example.com");

        // Act
        var isFull = eventEntity.IsFull;

        // Assert
        isFull.Should().BeTrue();
    }

    [Fact]
    public void IsFull_WhenBelowMaxCapacity_ShouldReturnFalse()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(30);
        var endDate = startDate.AddDays(2);
        var direccion = new Location("Venue", "Direccion", "Ciudad", "ST", "12345", "USA");
        var eventEntity = new Event("Tech Conference", "Description", direccion, startDate, endDate, 500, "org-123");
        eventEntity.Publicar();
        eventEntity.RegisterAttendee("user-1", "User One", "user1@example.com");

        // Act
        var isFull = eventEntity.IsFull;

        // Assert
        isFull.Should().BeFalse();
    }
}
*/