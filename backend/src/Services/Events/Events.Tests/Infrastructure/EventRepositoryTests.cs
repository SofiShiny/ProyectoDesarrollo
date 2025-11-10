/*using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Domain.ValueObjects;
using Events.Infrastructure.Persistence;
using Events.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Events.Tests.Infrastructure;

public class EventRepositoryTests
{
    private readonly EventsDbContext _context;
    private readonly EventRepository _repository;

    public EventRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<EventsDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new EventsDbContext(options);
        _repository = new EventRepository(_context);
    }

    [Fact]
    public async Task AddAsync_ShouldAddEventToDatabase()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(30);
        var endDate = startDate.AddDays(2);
        var direccion = new Location("Venue", "Direccion", "Ciudad", "State", "12345", "USA");
        var eventEntity = new Event("Tech Conference", "Description", direccion, startDate, endDate, 500, "organizer-123");

        // Act
        var result = await _repository.AddAsync(eventEntity, CancellationToken.None);
        await _context.SaveChangesAsync();

        // Assert
        result.Should().NotBeNull();
        var savedEvent = await _context.Events.FindAsync(result.Id);
        savedEvent.Should().NotBeNull();
        savedEvent!.Title.Should().Be("Tech Conference");
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingId_ShouldReturnEvent()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(30);
        var endDate = startDate.AddDays(2);
        var direccion = new Location("Venue", "Direccion", "Ciudad", "State", "12345", "USA");
        var eventEntity = new Event("Tech Conference", "Description", direccion, startDate, endDate, 500, "organizer-123");
        await _context.Events.AddAsync(eventEntity);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(eventEntity.Id, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(eventEntity.Id);
        result.Title.Should().Be("Tech Conference");
    }

    [Fact]
    public async Task GetByIdAsync_WithNonExistentId_ShouldReturnNull()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var result = await _repository.GetByIdAsync(nonExistentId, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateEvent()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(30);
        var endDate = startDate.AddDays(2);
        var direccion = new Location("Venue", "Direccion", "Ciudad", "State", "12345", "USA");
        var eventEntity = new Event("Original Title", "Description", direccion, startDate, endDate, 500, "organizer-123");
        await _context.Events.AddAsync(eventEntity);
        await _context.SaveChangesAsync();

        // Act
        eventEntity.Publicar();
        await _repository.UpdateAsync(eventEntity, CancellationToken.None);
        await _context.SaveChangesAsync();

        // Assert
        var updatedEvent = await _context.Events.FindAsync(eventEntity.Id);
        updatedEvent.Should().NotBeNull();
        updatedEvent!.Status.Should().Be(EventStatus.Publicado);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveEvent()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(30);
        var endDate = startDate.AddDays(2);
        var direccion = new Location("Venue", "Direccion", "Ciudad", "State", "12345", "USA");
        var eventEntity = new Event("Tech Conference", "Description", direccion, startDate, endDate, 500, "organizer-123");
        await _context.Events.AddAsync(eventEntity);
        await _context.SaveChangesAsync();

        // Act
        await _repository.DeleteAsync(eventEntity.Id, CancellationToken.None);
        await _context.SaveChangesAsync();

        // Assert
        var deletedEvent = await _context.Events.FindAsync(eventEntity.Id);
        deletedEvent.Should().BeNull();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEvents()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(30);
        var endDate = startDate.AddDays(2);
        var direccion = new Location("Venue", "Direccion", "Ciudad", "State", "12345", "USA");
        
        var event1 = new Event("Conference 1", "Description 1", direccion, startDate, endDate, 100, "organizer-123");
        var event2 = new Event("Conference 2", "Description 2", direccion, startDate.AddDays(10), endDate.AddDays(10), 200, "organizer-456");
        
        await _context.Events.AddRangeAsync(event1, event2);
        await _context.SaveChangesAsync();

        // Act
        var results = await _repository.GetAllAsync(CancellationToken.None);

        // Assert
        results.Should().HaveCount(2);
        results.Should().Contain(e => e.Title == "Conference 1");
        results.Should().Contain(e => e.Title == "Conference 2");
    }
}
*/