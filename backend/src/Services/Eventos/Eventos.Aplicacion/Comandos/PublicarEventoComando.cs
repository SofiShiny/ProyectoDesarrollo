using BloquesConstruccion.Aplicacion.Comun;
using BloquesConstruccion.Aplicacion.Comandos;

namespace Eventos.Aplicacion.Comandos;

public record PublicarEventoComando(Guid EventoId) : IComando<Resultado>;
