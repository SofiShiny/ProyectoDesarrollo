using BuildingBlocks.Application.Common;
using BuildingBlocks.Application.Comandos;

namespace Eventos.Aplicacion.Comandos;

public record PublicarEventoComando(Guid EventoId) : IComando<Result>;
