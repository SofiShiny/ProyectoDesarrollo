using BloquesConstruccion.Aplicacion.Comun;
using BloquesConstruccion.Aplicacion.Comandos;

namespace Eventos.Aplicacion.Comandos;

public record RegistrarAsistenteComando(
    Guid EventoId,
    string UsuarioId,
    string NombreUsuario,
    string Correo
) : IComando<Resultado>;
