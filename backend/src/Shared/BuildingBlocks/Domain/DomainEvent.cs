using System;

namespace BuildingBlocks.Domain;

public abstract class DomainEvent
{
    public Guid AggregateId { get; protected set; }
    public DateTime OccurredAt { get; protected set; } = DateTime.UtcNow;
}
