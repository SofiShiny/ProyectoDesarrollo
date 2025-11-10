using MediatR;
using Microsoft.AspNetCore.Mvc;
using Events.Domain.Repositories;

namespace Events.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IEventRepository _eventRepository;

    public EventsController(IMediator mediator, IEventRepository eventRepository)
    {
        _mediator = mediator;
        _eventRepository = eventRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        var events = await _eventRepository.GetAllAsync();
        return Ok(events);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(Guid id)
    {
        var eventEntity = await _eventRepository.GetByIdAsync(id);
        if (eventEntity == null)
            return NotFound();

        return Ok(eventEntity);
    }

    [HttpGet("organizer/{organizerId}")]
    public async Task<IActionResult> GetEventsByOrganizer(string organizerId)
    {
        var events = await _eventRepository.GetEventsByOrganizerAsync(organizerId);
        return Ok(events);
    }

    [HttpGet("published")]
    public async Task<IActionResult> GetPublishedEvents()
    {
        var events = await _eventRepository.GetPublishedEventsAsync();
        return Ok(events);
    }
}
