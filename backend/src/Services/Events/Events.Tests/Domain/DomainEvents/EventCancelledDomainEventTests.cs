/*using Events.Domain.DomainEvents;
using FluentAssertions;
using Xunit;
using System;
using BuildingBlocks.Domain;

namespace Events.Tests.Domain.DomainEvents;

public class EventCancelledDomainEventTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties_WhenCalled()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var reason = "Venue unavailable";
        var cancelledAt = DateTime.UtcNow;

        // Act
        var domainEvent = new EventCancelledDomainEvent(eventId, reason, cancelledAt);

        // Assert
        domainEvent.EventId.Should().Be(eventId);
        domainEvent.Reason.Should().Be(reason);
        domainEvent.CancelledAt.Should().Be(cancelledAt);
        domainEvent.OccurredOn.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void EventCancelledDomainEvent_ShouldImplementIDomainEvent()
    {
        // Arrange & Act
        var domainEvent = new EventCancelledDomainEvent(Guid.NewGuid(), "Test", DateTime.UtcNow);

        // Assert
        domainEvent.Should().BeAssignableTo<IDomainEvent>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_ShouldAcceptEmptyOrNullReason(string? reason)
    {
        // Arrange & Act
        var domainEvent = new EventCancelledDomainEvent(Guid.NewGuid(), reason!, DateTime.UtcNow);

        // Assert
        domainEvent.Reason.Should().Be(reason);
    }

    [Fact]
    public void Constructor_ShouldAcceptEmptyGuid()
    {
        // Arrange & Act
        var domainEvent = new EventCancelledDomainEvent(Guid.Empty, "Cancelled", DateTime.UtcNow);

        // Assert
        domainEvent.EventId.Should().Be(Guid.Empty);
    }

    [Fact]
    public void CancelledAt_ShouldAcceptDifferentTimeValues()
    {
        // Arrange
        var pastDate = DateTime.UtcNow.AddDays(-5);
        var futureDate = DateTime.UtcNow.AddDays(5);

        // Act
        var pastEvent = new EventCancelledDomainEvent(Guid.NewGuid(), "Test", pastDate);
        var futureEvent = new EventCancelledDomainEvent(Guid.NewGuid(), "Test", futureDate);

        // Assert
        pastEvent.CancelledAt.Should().Be(pastDate);
        futureEvent.CancelledAt.Should().Be(futureDate);
    }
}
*/