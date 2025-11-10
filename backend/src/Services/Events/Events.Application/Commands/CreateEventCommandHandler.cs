using BuildingBlocks.Application;
using BuildingBlocks.Application.Common;
using Eventos.Dominio.Entidades;
using Eventos.Dominio.Repositorios;
using Eventos.Dominio.ObjetosDeValor;
using Eventos.Aplicacion.DTOs;
using MediatR;

namespace Eventos.Aplicacion.Comandos;

public class CrearEventoCommandHandler : IRequestHandler<CrearEventoCommand, Result<EventoDto>>
{
    private readonly IRepositorioEvento _repositorioEvento;

    public CrearEventoCommandHandler(IRepositorioEvento repositorioEvento)
    {
        _repositorioEvento = repositorioEvento;
    }

    public async Task<Result<EventoDto>> Handle(CrearEventoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Ubicacion == null)
            {
                return Result<EventoDto>.Failure("La ubicación es obligatoria");
            }

            var ubicacion = new Ubicacion(
                request.Ubicacion.NombreLugar ?? string.Empty,
                request.Ubicacion.Direccion ?? string.Empty,
                request.Ubicacion.Ciudad ?? string.Empty,
                request.Ubicacion.Region ?? string.Empty,
                request.Ubicacion.CodigoPostal ?? string.Empty,
                request.Ubicacion.Pais ?? string.Empty
            );

            var evento = new Evento(
                request.Titulo,
                request.Descripcion,
                ubicacion,
                request.FechaInicio,
                request.FechaFin,
                request.MaximoAsistentes,
                request.OrganizadorId
            );

            await _repositorioEvento.AgregarAsync(evento, cancellationToken);

            var dto = MapToDto(evento);
            return Result<EventoDto>.Success(dto);
        }
        catch (ArgumentException ex)
        {
            return Result<EventoDto>.Failure(ex.Message);
        }
    }

    private EventoDto MapToDto(Evento evento)
    {
        return new EventoDto
        {
            Id = evento.Id,
            Titulo = evento.Titulo,
            Descripcion = evento.Descripcion,
            Ubicacion = new LocationDto
            {
                VenueName = evento.Ubicacion?.NombreLugar ?? string.Empty,
                Venue = evento.Ubicacion?.NombreLugar ?? string.Empty,
                Address = evento.Ubicacion?.Direccion ?? string.Empty,
                City = evento.Ubicacion?.Ciudad ?? string.Empty,
                State = evento.Ubicacion?.Region ?? string.Empty,
                ZipCode = evento.Ubicacion?.CodigoPostal ?? string.Empty,
                Country = evento.Ubicacion?.Pais ?? string.Empty
            },
            FechaInicio = evento.FechaInicio,
            FechaFin = evento.FechaFin,
            MaximoAsistentes = evento.MaximoAsistentes,
            ConteoAsistentesActual = evento.ConteoAsistentesActual,
            Estado = evento.Estado.ToString(),
            OrganizadorId = evento.OrganizadorId,
            CreadoEn = evento.CreatedAt
        };
    }
}
