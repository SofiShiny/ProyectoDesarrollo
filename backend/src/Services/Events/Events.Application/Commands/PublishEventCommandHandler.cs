using BuildingBlocks.Application;
using BuildingBlocks.Application.Common;
using Events.Domain.Repositories;
using MediatR;

namespace Events.Application.Commands;

public class PublishEventCommandHandler : IRequestHandler<PublishEventCommand, Result>
{
    private readonly IEventRepository _eventRepository;

    public PublishEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Result> Handle(PublishEventCommand request, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetByIdAsync(request.EventId, cancellationToken);
        
        if (@event == null)
            return Result.Failure("Event not found");

        try
        {
            @event.Publish();
            await _eventRepository.UpdateAsync(@event, cancellationToken);
            return Result.Success();
        }
        catch (InvalidOperationException ex)
        {
            return Result.Failure(ex.Message);
        }
    }
}
