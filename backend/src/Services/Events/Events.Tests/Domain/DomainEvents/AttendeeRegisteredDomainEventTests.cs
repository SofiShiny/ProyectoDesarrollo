/*using Events.Domain.DomainEvents;
using BuildingBlocks.Domain;
using FluentAssertions;
using Xunit;
using System;

namespace Events.Tests.Domain.DomainEvents;

public class AttendeeRegisteredDomainEventTests
{
    [Fact]
    public void Constructor_ShouldInitializeAllProperties_WhenCalled()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var attendeeId = Guid.NewGuid();
        var attendeeName = "John Doe";
        var attendeeEmail = "john@example.com";

        // Act
        var domainEvent = new AttendeeRegisteredDomainEvent(eventId, attendeeId, attendeeName, attendeeEmail);

        // Assert
        domainEvent.EventId.Should().Be(eventId);
        domainEvent.AttendeeId.Should().Be(attendeeId);
        domainEvent.AttendeeName.Should().Be(attendeeName);
        domainEvent.AttendeeEmail.Should().Be(attendeeEmail);
        domainEvent.OccurredOn.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void AttendeeRegisteredDomainEvent_ShouldImplementIDomainEvent()
    {
        // Arrange & Act
        var domainEvent = new AttendeeRegisteredDomainEvent(
            Guid.NewGuid(), 
            Guid.NewGuid(), 
            "Test", 
            "test@test.com");

        // Assert
        domainEvent.Should().BeAssignableTo<IDomainEvent>();
    }

    [Theory]
    [InlineData("", "")]
    [InlineData(null, null)]
    [InlineData("ValidName", "")]
    [InlineData("", "valid@email.com")]
    public void Constructor_ShouldAcceptEmptyOrNullStrings(string? name, string? email)
    {
        // Arrange & Act
        var domainEvent = new AttendeeRegisteredDomainEvent(
            Guid.NewGuid(), 
            Guid.NewGuid(), 
            name!, 
            email!);

        // Assert
        domainEvent.AttendeeName.Should().Be(name);
        domainEvent.AttendeeEmail.Should().Be(email);
    }

    [Fact]
    public void Constructor_ShouldAcceptEmptyGuids()
    {
        // Arrange & Act
        var domainEvent = new AttendeeRegisteredDomainEvent(
            Guid.Empty, 
            Guid.Empty, 
            "Test", 
            "test@test.com");

        // Assert
        domainEvent.EventId.Should().Be(Guid.Empty);
        domainEvent.AttendeeId.Should().Be(Guid.Empty);
    }
}
*/