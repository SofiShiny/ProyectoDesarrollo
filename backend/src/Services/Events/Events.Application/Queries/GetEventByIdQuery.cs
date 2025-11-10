using BuildingBlocks.Application.Common;
using BuildingBlocks.Application.Queries;
using Eventos.Aplicacion.DTOs;

namespace Eventos.Aplicacion.Queries;

public record ObtenerEventoPorIdQuery(Guid EventoId) : IQuery<Result<EventoDto>>;
