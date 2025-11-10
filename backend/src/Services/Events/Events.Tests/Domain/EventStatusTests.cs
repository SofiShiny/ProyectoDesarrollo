/*using Events.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace Events.Tests.Domain;

public class EventStatusTests
{
    [Fact]
    public void EventStatus_ShouldHaveDraftValue()
    {
        // Act
        var status = EventStatus.Draft;

        // Assert
        status.Should().Be(EventStatus.Draft);
        ((int)status).Should().Be(0);
    }

    [Fact]
    public void EventStatus_ShouldHavePublishedValue()
    {
        // Act
        var status = EventStatus.Published;

        // Assert
        status.Should().Be(EventStatus.Published);
        ((int)status).Should().Be(1);
    }

    [Fact]
    public void EventStatus_ShouldHaveCancelledValue()
    {
        // Act
        var status = EventStatus.Cancelled;

        // Assert
        status.Should().Be(EventStatus.Cancelled);
        ((int)status).Should().Be(2);
    }

    [Fact]
    public void EventStatus_ShouldHaveCompletedValue()
    {
        // Act
        var status = EventStatus.Completed;

        // Assert
        status.Should().Be(EventStatus.Completed);
        ((int)status).Should().Be(3);
    }

    [Theory]
    [InlineData(EventStatus.Draft, "Draft")]
    [InlineData(EventStatus.Published, "Published")]
    [InlineData(EventStatus.Cancelled, "Cancelled")]
    [InlineData(EventStatus.Completed, "Completed")]
    public void EventStatus_ShouldConvertToString(EventStatus status, string expected)
    {
        // Act
        var result = status.ToString();

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void EventStatus_ShouldAllowComparison()
    {
        // Arrange
        var draft = EventStatus.Draft;
        var published = EventStatus.Published;

        // Act & Assert
        draft.Should().NotBe(published);
        draft.Should().Be(EventStatus.Draft);
    }

    [Fact]
    public void EventStatus_ShouldSupportAllValues()
    {
        // Arrange & Act
        var values = Enum.GetValues<EventStatus>();

        // Assert
        values.Should().HaveCount(4);
        values.Should().Contain(EventStatus.Draft);
        values.Should().Contain(EventStatus.Published);
        values.Should().Contain(EventStatus.Cancelled);
        values.Should().Contain(EventStatus.Completed);
    }
}
*/