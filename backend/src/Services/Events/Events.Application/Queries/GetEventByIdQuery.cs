using BuildingBlocks.Application.Common;
using BuildingBlocks.Application.Queries;
using Events.Application.DTOs;

namespace Events.Application.Queries;

public record GetEventByIdQuery(Guid EventId) : IQuery<Result<EventDto>>;
