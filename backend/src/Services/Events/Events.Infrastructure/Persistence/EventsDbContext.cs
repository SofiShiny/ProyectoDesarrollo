using Microsoft.EntityFrameworkCore;
using Eventos.Dominio.Entidades;
using Events.Infrastructure.Persistence.Configurations;
using BuildingBlocks.Domain;

namespace Events.Infrastructure.Persistence;

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

        modelBuilder.Ignore<DomainEvent>();

        modelBuilder.ApplyConfiguration(new EventConfiguration());
        modelBuilder.ApplyConfiguration(new AttendeeConfiguration());
    }
}
