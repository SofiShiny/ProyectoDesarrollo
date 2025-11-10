using Microsoft.EntityFrameworkCore;
using Eventos.Dominio.Entidades;
using Eventos.Dominio.Repositorios;
using Eventos.Dominio.Enumeraciones;
using Events.Infrastructure.Persistence;

namespace Events.Infrastructure.Repositories;

public class EventRepository : IRepositorioEvento
{
    private readonly EventsDbContext _context;

    public EventRepository(EventsDbContext context)
    {
        _context = context;
    }

    public async Task<Evento?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Eventos
            .Include(e => e.Asistentes)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Evento>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Eventos
            .Include(e => e.Asistentes)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Evento>> ObtenerEventosPublicadosAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Eventos
            .Include(e => e.Asistentes)
            .Where(e => e.Estado == EstadoEvento.Publicado)
            .OrderBy(e => e.FechaInicio)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Evento>> ObtenerEventosPorOrganizadorAsync(string organizadorId, CancellationToken cancellationToken = default)
    {
        return await _context.Eventos
            .Include(e => e.Asistentes)
            .Where(e => e.OrganizadorId == organizadorId)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AgregarAsync(Evento evento, CancellationToken cancellationToken = default)
    {
        await _context.Eventos.AddAsync(evento, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task ActualizarAsync(Evento evento, CancellationToken cancellationToken = default)
    {
        _context.Eventos.Update(evento);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task EliminarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var evento = await ObtenerPorIdAsync(id, cancellationToken);
        if (evento != null)
        {
            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> ExisteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Eventos.AnyAsync(e => e.Id == id, cancellationToken);
    }
}
