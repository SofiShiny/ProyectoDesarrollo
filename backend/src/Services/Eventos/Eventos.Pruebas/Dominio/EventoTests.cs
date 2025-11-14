using Eventos.Dominio.Entidades;
using Eventos.Dominio.Enumeraciones;
using Eventos.Dominio.ObjetosDeValor;
using FluentAssertions;
using Xunit;

namespace Eventos.Pruebas.Dominio;

public class EventoTests
{
    [Fact]
    public void CrearEvento_ConDatosValidos_DeberiaTenerExito()
    {
        // Preparar
        var titulo = "Taller de Arte";
        var descripcion = "Exposicion de Obras";
        var fechaInicio = DateTime.UtcNow.AddDays(30);
        var fechaFin = fechaInicio.AddDays(2);
        var ubicacion = new Ubicacion("Calle7", "El Marques", "Caracas", "DF", "1073", "Venezuela");
        var maximo =500;
        var organizadorId = "organizador-001";

        // Actuar
        var evento = new Evento(titulo, descripcion, ubicacion, fechaInicio, fechaFin, maximo, organizadorId);

        // Comprobar
        evento.Should().NotBeNull();
        evento.Titulo.Should().Be(titulo);
        evento.Descripcion.Should().Be(descripcion);
        evento.Ubicacion.Should().Be(ubicacion);
        evento.FechaInicio.Should().Be(fechaInicio);
        evento.FechaFin.Should().Be(fechaFin);
        evento.MaximoAsistentes.Should().Be(maximo);
        evento.Estado.Should().Be(EstadoEvento.Borrador);
        evento.Asistentes.Should().BeEmpty();
    }

    [Fact]
    public void CrearEvento_ConFechaFinAntesDeFechaInicio_DeberiaLanzarExcepcion()
    {
        // Preparar
        var fechaInicio = DateTime.UtcNow.AddDays(30);
        var fechaFin = fechaInicio.AddDays(-1);
        var ubicacion = new Ubicacion("Calle7", "El Marques", "Caracas", "DF", "1073", "Venezuela");

        // Actuar
        Action act = () => new Evento("Titulo", "Descripcion", ubicacion, fechaInicio, fechaFin,100, "organizador-001");

        // Comprobar
        act.Should().Throw<ArgumentException>()
            .WithMessage("La fecha de finalización debe ser posterior a la fecha de inicio*");
    }

    [Fact]
    public void CrearEvento_ConMaximoAsistentesNegativo_DeberiaLanzarExcepcion()
    {
        // Preparar
        var fechaInicio = DateTime.UtcNow.AddDays(30);
        var fechaFin = fechaInicio.AddDays(2);
        var ubicacion = new Ubicacion("Calle7", "El Marques", "Caracas", "DF", "1073", "Venezuela");

        // Actuar
        Action act = () => new Evento("Titulo", "Descripcion", ubicacion, fechaInicio, fechaFin, -1, "organizador-001");

        // Comprobar
        act.Should().Throw<ArgumentException>()
            .WithMessage("El número máximo de asistentes debe ser mayor que cero*");
    }

    [Fact]
    public void Publicar_EventoEnBorrador_DeberiaCambiarEstadoAPublicado()
    {
        // Preparar
        var fechaInicio = DateTime.UtcNow.AddDays(30);
        var fechaFin = fechaInicio.AddDays(2);
        var ubicacion = new Ubicacion("Calle7", "El Marques", "Caracas", "DF", "1073", "Venezuela");
        var evento = new Evento("Taller de Arte", "Exposicion", ubicacion, fechaInicio, fechaFin,500, "organizador-001");

        // Actuar
        evento.Publicar();

        // Comprobar
        evento.Estado.Should().Be(EstadoEvento.Publicado);
    }

    [Fact]
    public void Publicar_EventoYaPublicado_DeberiaLanzarExcepcion()
    {
        // Preparar
        var fechaInicio = DateTime.UtcNow.AddDays(30);
        var fechaFin = fechaInicio.AddDays(2);
        var ubicacion = new Ubicacion("Calle7", "El Marques", "Caracas", "DF", "1073", "Venezuela");
        var evento = new Evento("Taller de Arte", "Exposicion", ubicacion, fechaInicio, fechaFin,500, "organizador-001");
        evento.Publicar();

        // Actuar
        Action act = () => evento.Publicar();

        // Comprobar
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("No se puede publicar el evento cuando está en estado*");
    }

    [Fact]
    public void Cancelar_EventoPublicado_DeberiaCambiarEstadoACancelado()
    {
        // Preparar
        var fechaInicio = DateTime.UtcNow.AddDays(30);
        var fechaFin = fechaInicio.AddDays(2);
        var ubicacion = new Ubicacion("Calle7", "El Marques", "Caracas", "DF", "1073", "Venezuela");
        var evento = new Evento("Taller de Arte", "Exposicion", ubicacion, fechaInicio, fechaFin,500, "organizador-001");
        evento.Publicar();

        // Actuar
        evento.Cancelar();

        // Comprobar
        evento.Estado.Should().Be(EstadoEvento.Cancelado);
    }

    [Fact]
    public void RegistrarAsistente_ConCupoDisponible_DeberiaRegistrar()
    {
        // Preparar
        var fechaInicio = DateTime.UtcNow.AddDays(30);
        var fechaFin = fechaInicio.AddDays(2);
        var ubicacion = new Ubicacion("Calle7", "El Marques", "Caracas", "DF", "1073", "Venezuela");
        var evento = new Evento("Taller de Arte", "Exposicion", ubicacion, fechaInicio, fechaFin,500, "organizador-001");
        evento.Publicar();
        var usuarioId = "usuario-001";
        var nombreUsuario = "Creonte Dioniso Lara Wilson";
        var correo = "cdlara@est.ucab.edu.ve";

        // Actuar
        evento.RegistrarAsistente(usuarioId, nombreUsuario, correo);

        // Comprobar
        evento.Asistentes.Should().ContainSingle();
        var asistente = evento.Asistentes.First();
        asistente.UsuarioId.Should().Be(usuarioId);
        asistente.NombreUsuario.Should().Be(nombreUsuario);
        asistente.Correo.Should().Be(correo);
    }

    [Fact]
    public void RegistrarAsistente_EventoNoPublicado_DeberiaLanzarExcepcion()
    {
        // Preparar
        var fechaInicio = DateTime.UtcNow.AddDays(30);
        var fechaFin = fechaInicio.AddDays(2);
        var ubicacion = new Ubicacion("Calle7", "El Marques", "Caracas", "DF", "1073", "Venezuela");
        var evento = new Evento("Taller de Arte", "Exposicion", ubicacion, fechaInicio, fechaFin,500, "organizador-001");

        // Actuar
        Action act = () => evento.RegistrarAsistente("usuario-001", "Creonte", "cdlara@est.ucab.edu.ve");

        // Comprobar
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("No se puede registrar en un evento no publicado");
    }

    [Fact]
    public void RegistrarAsistente_EventoLleno_DeberiaLanzarExcepcion()
    {
        // Preparar
        var fechaInicio = DateTime.UtcNow.AddDays(30);
        var fechaFin = fechaInicio.AddDays(2);
        var ubicacion = new Ubicacion("Calle7", "El Marques", "Caracas", "DF", "1073", "Venezuela");
        var evento = new Evento("Taller de Arte", "Exposicion", ubicacion, fechaInicio, fechaFin,1, "organizador-001");
        evento.Publicar();
        evento.RegistrarAsistente("usuario-001", "Creonte", "cdlara@est.ucab.edu.ve");

        // Actuar
        Action act = () => evento.RegistrarAsistente("usuario-002", "Electra", "electra@example.com");

        // Comprobar
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("El evento está completo");
    }

    [Fact]
    public void EstaCompleto_AlAlcanzarCapacidad_DeberiaRetornarTrue()
    {
        // Preparar
        var fechaInicio = DateTime.UtcNow.AddDays(30);
        var fechaFin = fechaInicio.AddDays(2);
        var ubicacion = new Ubicacion("Calle7", "El Marques", "Caracas", "DF", "1073", "Venezuela");
        var evento = new Evento("Taller de Arte", "Exposicion", ubicacion, fechaInicio, fechaFin,2, "organizador-001");
        evento.Publicar();
        evento.RegistrarAsistente("usuario-001", "Creonte", "cdlara@est.ucab.edu.ve");
        evento.RegistrarAsistente("usuario-002", "Electra", "electra@example.com");

        // Actuar
        var lleno = evento.EstaCompleto;

        // Comprobar
        lleno.Should().BeTrue();
    }

    [Fact]
    public void EstaCompleto_PorDebajoDeCapacidad_DeberiaRetornarFalse()
    {
        // Preparar
        var fechaInicio = DateTime.UtcNow.AddDays(30);
        var fechaFin = fechaInicio.AddDays(2);
        var ubicacion = new Ubicacion("Calle7", "El Marques", "Caracas", "DF", "1073", "Venezuela");
        var evento = new Evento("Taller de Arte", "Exposicion", ubicacion, fechaInicio, fechaFin,500, "organizador-001");
        evento.Publicar();
        evento.RegistrarAsistente("usuario-001", "Creonte", "cdlara@est.ucab.edu.ve");

        // Actuar
        var lleno = evento.EstaCompleto;

        // Comprobar
        lleno.Should().BeFalse();
    }
}
