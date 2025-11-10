using Bloques.Aplicacion.Comun;
using Bloques.Aplicacion.Comandos;

namespace Eventos.Aplicacion.Comandos;

public record RegistrarAsistenteComando(
    Guid EventoId,
    string UsuarioId,
    string NombreUsuario,
    string Correo
) : IComando<Resultado>;
