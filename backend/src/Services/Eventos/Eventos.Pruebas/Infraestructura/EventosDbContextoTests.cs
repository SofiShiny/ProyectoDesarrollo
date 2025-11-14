using Eventos.Dominio.Entidades;
using Eventos.Dominio.ObjetosDeValor;
using Eventos.Infraestructura.Persistencia;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Eventos.Pruebas.Infraestructura;

public class EventosDbContextoTests
{
    private EventosDbContext CrearDbContexto()
    {
        var options = new DbContextOptionsBuilder<EventosDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new EventosDbContext(options);
    }

    [Fact]
    public void EventosDbContexto_DeberiaTenerDbSetEventos()
    {
        // Preparar
        using var context = CrearDbContexto();

        // Actuar & Comprobar
        context.Eventos.Should().NotBeNull();
    }

    [Fact]
    public async Task EventosDbContexto_DeberiaGuardarEvento()
    {
        // Preparar
        using var context = CrearDbContexto();
        var direccion = new Ubicacion("Calle7", "El Marques", "Caracas", "DF", "1073", "Venezuela");
        var @evento = new Evento(
            "Taller de Arte",
            "Exposicion de Obras",
            direccion,
            DateTime.UtcNow.AddDays(30),
            DateTime.UtcNow.AddDays(30).AddHours(4),
            100,
            "organizador-001");

        // Actuar
        context.Eventos.Add(@evento);
        await context.SaveChangesAsync();

        // Comprobar
        var savedEvento = await context.Eventos.FindAsync(@evento.Id);
        savedEvento.Should().NotBeNull();
        savedEvento!.Titulo.Should().Be("Taller de Arte");
    }

    [Fact]
    public async Task EventosDbContexto_DeberiaRastrearCambios()
    {
        // Preparar
        using var context = CrearDbContexto();
        var direccion = new Ubicacion("Calle7", "El Marques", "Caracas", "DF", "1073", "Venezuela");
        var @evento = new Evento(
            "Nombre Original",
            "Descripcion",
            direccion,
            DateTime.UtcNow.AddDays(30),
            DateTime.UtcNow.AddDays(30).AddHours(4),
            100,
            "organizador-001");

        context.Eventos.Add(@evento);
        await context.SaveChangesAsync();

        // Actuar
        @evento.Actualizar("Nombre Actualizado", "Nueva Descripcion", direccion, @evento.FechaInicio, @evento.FechaFin, 100);
        await context.SaveChangesAsync();

        // Comprobar
        var updatedEvento = await context.Eventos.FindAsync(@evento.Id);
        updatedEvento!.Titulo.Should().Be("Nombre Actualizado");
        updatedEvento.Descripcion.Should().Be("Nueva Descripcion");
    }

    [Fact]
    public async Task EventosDbContexto_DeberiaEliminarEvento()
    {
        // Preparar
        using var context = CrearDbContexto();
        var direccion = new Ubicacion("Calle7", "El Marques", "Caracas", "DF", "1073", "Venezuela");
        var @evento = new Evento(
            "Evento a Eliminar",
            "Descripcion",
            direccion,
            DateTime.UtcNow.AddDays(30),
            DateTime.UtcNow.AddDays(30).AddHours(4),
            100,
            "organizador-001");

        context.Eventos.Add(@evento);
        await context.SaveChangesAsync();

        // Actuar
        context.Eventos.Remove(@evento);
        await context.SaveChangesAsync();

        // Comprobar
        var deletedEvento = await context.Eventos.FindAsync(@evento.Id);
        deletedEvento.Should().BeNull();
    }
}