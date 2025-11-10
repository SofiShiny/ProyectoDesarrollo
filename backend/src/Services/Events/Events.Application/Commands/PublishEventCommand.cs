using BuildingBlocks.Application.Common;
using BuildingBlocks.Application.Commands;

namespace Events.Application.Commands;

public record PublishEventCommand(Guid EventId) : ICommand<Result>;
