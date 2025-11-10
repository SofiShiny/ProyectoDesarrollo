using BuildingBlocks.Domain;

namespace Eventos.Dominio.EventosDeDominio;

public class EventoCanceladoEventoDominio : DomainEvent
{
    public Guid EventoId { get; }
    public string TituloEvento { get; }

    public EventoCanceladoEventoDominio(Guid eventoId, string tituloEvento)
    {
        EventoId = eventoId;
        TituloEvento = tituloEvento;
    }
}
