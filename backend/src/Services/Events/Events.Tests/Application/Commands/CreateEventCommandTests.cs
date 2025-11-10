using Events.Application.Commands;
using Events.Application.DTOs;
using FluentAssertions;
using Xunit;

namespace Events.Tests.Application.Commands;

public class CreateEventCommandTests
{
    [Fact]
    public void Constructor_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        var location = new LocationDto
        {
            VenueName = "Centro de Convenciones",
            Address = "Av. Principal 123",
            City = "Caracas",
            State = "DF",
            ZipCode = "1010",
            Country = "Venezuela"
        };

        var startDate = DateTime.UtcNow.AddDays(10);
        var endDate = startDate.AddDays(2);

        // Act
        var command = new CreateEventCommand(
            Title: "Conferencia de Tecnología",
            Description: "Evento anual sobre innovación",
            Location: location,
            StartDate: startDate,
            EndDate: endDate,
            MaxAttendees: 300,
            OrganizerId: "org-001"
        );

        // Assert
        command.Title.Should().Be("Conferencia de Tecnología");
        command.Description.Should().Be("Evento anual sobre innovación");
        command.Location.Should().Be(location);
        command.StartDate.Should().Be(startDate);
        command.EndDate.Should().Be(endDate);
        command.MaxAttendees.Should().Be(300);
        command.OrganizerId.Should().Be("org-001");
    }

    [Fact]
    public void Equality_ShouldWorkCorrectly()
    {
        // Arrange
        var location = new LocationDto
        {
            VenueName = "Centro de Convenciones",
            Address = "Av. Principal 123",
            City = "Caracas",
            State = "DF",
            ZipCode = "1010",
            Country = "Venezuela"
        };

        var command1 = new CreateEventCommand(
            Title: "Evento",
            Description: "Descripción",
            Location: location,
            StartDate: DateTime.UtcNow,
            EndDate: DateTime.UtcNow.AddDays(1),
            MaxAttendees: 100,
            OrganizerId: "org-001"
        );

        var command2 = command1 with { };

        // Assert
        command1.Should().Be(command2);
        command1.GetHashCode().Should().Be(command2.GetHashCode());
    }
}