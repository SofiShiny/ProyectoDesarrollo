using System;

namespace Bloques.Dominio;

public abstract class EventoDominio
{
    public Guid IdAgregado { get; protected set; }
    public DateTime OcurrioEn { get; protected set; } = DateTime.UtcNow;
}
