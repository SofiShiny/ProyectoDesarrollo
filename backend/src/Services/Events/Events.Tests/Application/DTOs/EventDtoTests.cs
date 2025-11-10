/*using Events.Application.DTOs;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Events.Tests.Application.DTOs;

public class EventDtoTests
{
    [Fact]
    public void EventDto_ShouldInitializeAllProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var title = "Tech Conference";
        var description = "Annual event";
        var startDate = DateTime.UtcNow.AddDays(30);
        var endDate = startDate.AddHours(8);
        var maxAttendees = 100;
        var status = "Published";

        // Act
        var dto = new EventDto
        {
            Id = id,
            Title = title,
            Description = description,
            StartDate = startDate,
            EndDate = endDate,
            MaxAttendees = maxAttendees,
            Status = status
        };

        // Assert
        dto.Id.Should().Be(id);
        dto.Title.Should().Be(title);
        dto.Description.Should().Be(description);
        dto.StartDate.Should().Be(startDate);
        dto.EndDate.Should().Be(endDate);
        dto.MaxAttendees.Should().Be(maxAttendees);
        dto.Status.Should().Be(status);
    }

    [Fact]
    public void EventDto_Location_ShouldBeSettable()
    {
        // Arrange
        var dto = new EventDto();
        var locationDto = new LocationDto
        {
            VenueName = "Convention Center",
            Address = "123 Main St",
            City = "Tech City",
            State = "TC",
            ZipCode = "12345"
        };

        // Act
        dto.Location = locationDto;

        // Assert
        dto.Location.Should().NotBeNull();
        dto.Location.VenueName.Should().Be("Convention Center");
        dto.Location.City.Should().Be("Tech City");
    }

    [Fact]
    public void EventDto_Attendees_ShouldBeSettableAndEnumerable()
    {
        // Arrange
        var dto = new EventDto();
        var attendees = new List<AttendeeDto>
        {
            new AttendeeDto { Id = Guid.NewGuid(), Name = "John Doe", Email = "john@test.com" },
            new AttendeeDto { Id = Guid.NewGuid(), Name = "Jane Smith", Email = "jane@test.com" }
        };

        // Act
        dto.Attendees = attendees;

        // Assert
        dto.Attendees.Should().HaveCount(2);
        dto.Attendees.Should().Contain(a => a.Name == "John Doe");
        dto.Attendees.Should().Contain(a => a.Name == "Jane Smith");
    }

    [Fact]
    public void EventDto_ShouldAllowNullValues()
    {
        // Arrange & Act
        var dto = new EventDto
        {
            Description = null,
            Location = null,
            Attendees = null
        };

        // Assert
        dto.Description.Should().BeNull();
        dto.Location.Should().BeNull();
        dto.Attendees.Should().BeNull();
    }
}
*/