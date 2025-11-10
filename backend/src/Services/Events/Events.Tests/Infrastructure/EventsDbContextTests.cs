/*using Events.Domain.Entities;
using Events.Domain.ValueObjects;
using Events.Infrastructure.Persistence;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Events.Tests.Infrastructure;

public class EventsDbContextTests
{
    private EventsDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<EventsDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new EventsDbContext(options);
    }

    [Fact]
    public void EventsDbContext_ShouldHaveEventsDbSet()
    {
        // Arrange
        using var context = CreateDbContext();

        // Act & Assert
        context.Events.Should().NotBeNull();
    }

    [Fact]
    public async Task EventsDbContext_ShouldSaveEvent()
    {
        // Arrange
        using var context = CreateDbContext();
        var location = new Location("Test Venue", "123 Main St", "City", "State", "12345", "USA");
        var @event = new Event(
            "Test Event", 
            "Description",
            location,
            DateTime.UtcNow.AddDays(30), 
            DateTime.UtcNow.AddDays(30).AddHours(4), 
            100,
            "organizer-123");

        // Act
        context.Events.Add(@event);
        await context.SaveChangesAsync();

        // Assert
        var savedEvent = await context.Events.FindAsync(@event.Id);
        savedEvent.Should().NotBeNull();
        savedEvent!.Title.Should().Be("Test Event");
    }

    [Fact]
    public async Task EventsDbContext_ShouldTrackChanges()
    {
        // Arrange
        using var context = CreateDbContext();
        var location = new Location("Test Venue", "123 Main St", "City", "State", "12345", "USA");
        var @event = new Event(
            "Original Name", 
            "Description",
            location,
            DateTime.UtcNow.AddDays(30), 
            DateTime.UtcNow.AddDays(30).AddHours(4), 
            100,
            "organizer-123");

        context.Events.Add(@event);
        await context.SaveChangesAsync();

        // Act
        @event.Update("Updated Name", "New Description", location, @event.StartDate, @event.EndDate, 100);
        await context.SaveChangesAsync();

        // Assert
        var updatedEvent = await context.Events.FindAsync(@event.Id);
        updatedEvent!.Title.Should().Be("Updated Name");
        updatedEvent.Description.Should().Be("New Description");
    }

    [Fact]
    public async Task EventsDbContext_ShouldDeleteEvent()
    {
        // Arrange
        using var context = CreateDbContext();
        var location = new Location("Test Venue", "123 Main St", "City", "State", "12345", "USA");
        var @event = new Event(
            "Event to Delete", 
            "Description",
            location,
            DateTime.UtcNow.AddDays(30), 
            DateTime.UtcNow.AddDays(30).AddHours(4), 
            100,
            "organizer-123");

        context.Events.Add(@event);
        await context.SaveChangesAsync();

        // Act
        context.Events.Remove(@event);
        await context.SaveChangesAsync();

        // Assert
        var deletedEvent = await context.Events.FindAsync(@event.Id);
        deletedEvent.Should().BeNull();
    }
}
*/