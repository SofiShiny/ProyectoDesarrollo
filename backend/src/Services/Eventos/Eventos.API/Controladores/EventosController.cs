using MediatR;
using Microsoft.AspNetCore.Mvc;
using Eventos.Dominio.Repositorios;

namespace Eventos.API.Controladores;

[ApiController]
[Route("api/[controller]")]
public class EventosController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IRepositorioEvento _repositorio;

    public EventosController(IMediator mediator, IRepositorioEvento repositorio)
    {
        _mediator = mediator;
        _repositorio = repositorio;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var eventos = await _repositorio.ObtenerTodosAsync();
        return Ok(eventos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(Guid id)
    {
        var evento = await _repositorio.ObtenerPorIdAsync(id);
        if (evento == null)
            return NotFound();

        return Ok(evento);
    }

    [HttpGet("organizador/{organizadorId}")]
    public async Task<IActionResult> ObtenerPorOrganizador(string organizadorId)
    {
        var eventos = await _repositorio.ObtenerEventosPorOrganizadorAsync(organizadorId);
        return Ok(eventos);
    }

    [HttpGet("publicados")]
    public async Task<IActionResult> ObtenerPublicados()
    {
        var eventos = await _repositorio.ObtenerEventosPublicadosAsync();
        return Ok(eventos);
    }
}
