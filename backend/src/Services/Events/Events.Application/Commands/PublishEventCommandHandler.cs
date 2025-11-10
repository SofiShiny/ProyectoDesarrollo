using BuildingBlocks.Application;
using BuildingBlocks.Application.Common;
using Eventos.Dominio.Repositorios;
using MediatR;

namespace Eventos.Aplicacion.Comandos;

public class PublicarEventoComandoHandler : IRequestHandler<PublicarEventoComando, Result>
{
    private readonly IRepositorioEvento _repositorioEvento;

    public PublicarEventoComandoHandler(IRepositorioEvento repositorioEvento)
    {
        _repositorioEvento = repositorioEvento;
    }

    public async Task<Result> Handle(PublicarEventoComando request, CancellationToken cancellationToken)
    {
        var evento = await _repositorioEvento.ObtenerPorIdAsync(request.EventoId, cancellationToken);
        
        if (evento == null)
            return Result.Failure("Evento no encontrado");

        try
        {
            evento.Publicar();
            await _repositorioEvento.ActualizarAsync(evento, cancellationToken);
            return Result.Success();
        }
        catch (InvalidOperationException ex)
        {
            return Result.Failure(ex.Message);
        }
    }
}
