/*using Events.Domain.DomainEvents;
using BuildingBlocks.Domain;
using FluentAssertions;
using Xunit;
using System;

namespace Events.Tests.Domain.DomainEvents;

public class EventPublishedDomainEventTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties_WhenCalled()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var eventName = "Tech Conference 2024";
        var publishedAt = DateTime.UtcNow;

        // Act
        var domainEvent = new EventPublishedDomainEvent(eventId, eventName, publishedAt);

        // Assert
        domainEvent.EventId.Should().Be(eventId);
        domainEvent.EventName.Should().Be(eventName);
        domainEvent.PublishedAt.Should().Be(publishedAt);
        domainEvent.OccurredOn.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void EventPublishedDomainEvent_ShouldBeOfTypeDomainEvent()
    {
        // Arrange & Act
        var domainEvent = new EventPublishedDomainEvent(Guid.NewGuid(), "Test", DateTime.UtcNow);

        // Assert
        domainEvent.Should().BeAssignableTo<IDomainEvent>();
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public void Constructor_ShouldAcceptEmptyGuid_WhenProvided(string guidString)
    {
        // Arrange
        var eventId = Guid.Parse(guidString);

        // Act
        var domainEvent = new EventPublishedDomainEvent(eventId, "Test", DateTime.UtcNow);

        // Assert
        domainEvent.EventId.Should().Be(Guid.Empty);
    }

    [Fact]
    public void Constructor_ShouldAcceptNullOrEmptyName_WhenProvided()
    {
        // Arrange & Act
        var domainEvent1 = new EventPublishedDomainEvent(Guid.NewGuid(), null!, DateTime.UtcNow);
        var domainEvent2 = new EventPublishedDomainEvent(Guid.NewGuid(), string.Empty, DateTime.UtcNow);

        // Assert
        domainEvent1.EventName.Should().BeNull();
        domainEvent2.EventName.Should().BeEmpty();
    }

    [Fact]
    public void PublishedAt_ShouldAcceptPastDates()
    {
        // Arrange
        var pastDate = DateTime.UtcNow.AddDays(-10);

        // Act
        var domainEvent = new EventPublishedDomainEvent(Guid.NewGuid(), "Test", pastDate);

        // Assert
        domainEvent.PublishedAt.Should().Be(pastDate);
    }

    [Fact]
    public void PublishedAt_ShouldAcceptFutureDates()
    {
        // Arrange
        var futureDate = DateTime.UtcNow.AddDays(10);

        // Act
        var domainEvent = new EventPublishedDomainEvent(Guid.NewGuid(), "Test", futureDate);

        // Assert
        domainEvent.PublishedAt.Should().Be(futureDate);
    }
}
*/