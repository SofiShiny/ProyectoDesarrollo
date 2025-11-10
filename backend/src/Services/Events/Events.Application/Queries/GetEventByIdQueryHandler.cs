using BuildingBlocks.Application;
using Events.Domain.Repositories;
using Events.Application.DTOs;
using MediatR;
using BuildingBlocks.Application.Common;

namespace Events.Application.Queries;

public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, Result<EventDto>>
{
    private readonly IEventRepository _eventRepository;

    public GetEventByIdQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Result<EventDto>> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetByIdAsync(request.EventId, cancellationToken);
        
        if (@event == null)
            return Result<EventDto>.Failure("Event not found");

        if (@event.Location == null)
            return Result<EventDto>.Failure("Event location data is invalid");

        var dto = new EventDto
        {
            Id = @event.Id,
            Title = @event.Title,
            Description = @event.Description,
            Location = new LocationDto
            {
                VenueName = @event.Location.VenueName,
                Venue = @event.Location.VenueName,
                Address = @event.Location.Address,
                City = @event.Location.City,
                State = @event.Location.State,
                ZipCode = @event.Location.PostalCode,
                Country = @event.Location.Country
            },
            StartDate = @event.StartDate,
            EndDate = @event.EndDate,
            MaxAttendees = @event.MaxAttendees,
            CurrentAttendeesCount = @event.CurrentAttendeesCount,
            Status = @event.Status.ToString(),
            OrganizerId = @event.OrganizerId,
            CreatedAt = @event.CreatedAt,
            Attendees = @event.Attendees.Select(a => new AttendeeDto
            {
                Id = a.Id,
                Name = a.UserName,
                Email = a.Email,
                RegisteredAt = a.RegisteredAt
            }).ToList()
        };

        return Result<EventDto>.Success(dto);
    }
}
