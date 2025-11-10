using BuildingBlocks.Application.Common;
using BuildingBlocks.Application.Commands;
using Eventos.Aplicacion.DTOs;

namespace Eventos.Aplicacion.Comandos;

public record CrearEventoCommand(
    string Titulo,
    string Descripcion,
    LocationDto Ubicacion,
    DateTime FechaInicio,
    DateTime FechaFin,
    int MaximoAsistentes,
    string OrganizadorId
) : ICommand<Result<EventoDto>>;
