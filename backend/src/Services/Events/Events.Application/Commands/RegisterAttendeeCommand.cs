using BuildingBlocks.Application.Common;
using BuildingBlocks.Application.Commands;

namespace Eventos.Aplicacion.Comandos;

public record RegistrarAsistenteCommand(
    Guid EventoId,
    string UsuarioId,
    string NombreUsuario,
    string Correo
) : ICommand<Result>;
