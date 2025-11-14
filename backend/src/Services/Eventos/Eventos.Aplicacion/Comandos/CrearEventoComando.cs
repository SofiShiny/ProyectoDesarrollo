using BloquesConstruccion.Aplicacion.Comun;
using BloquesConstruccion.Aplicacion.Comandos;
using Eventos.Aplicacion.DTOs;

namespace Eventos.Aplicacion.Comandos;

public record CrearEventoComando(
    string Titulo,
    string Descripcion,
    UbicacionDto Ubicacion,
    DateTime FechaInicio,
    DateTime FechaFin,
    int MaximoAsistentes,
    string OrganizadorId
) : IComando<Resultado<EventoDto>>;
