using BuildingBlocks.Domain;

namespace Eventos.Dominio.EventosDeDominio;

public class AsistenteRegistradoEventoDominio : DomainEvent
{
    public Guid EventoId { get; }
    public string UsuarioId { get; }
    public string NombreUsuario { get; }

    public AsistenteRegistradoEventoDominio(Guid eventoId, string usuarioId, string nombreUsuario)
    {
        EventoId = eventoId;
        UsuarioId = usuarioId;
        NombreUsuario = nombreUsuario;
    }
}
