using Microsoft.EntityFrameworkCore;
using Eventos.Dominio.Entidades;
using Eventos.Infraestructura.Persistence.Configurations;
using Bloques.Dominio;

namespace Eventos.Infraestructura.Persistence;

public class EventsDbContext : DbContext
{
    public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options)
    {
    }

    public DbSet<Evento> Eventos => Set<Evento>();
    public DbSet<Asistente> Asistentes => Set<Asistente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<EventoDominio>();

        modelBuilder.ApplyConfiguration(new EventConfiguration());
        modelBuilder.ApplyConfiguration(new AttendeeConfiguration());
    }
}
