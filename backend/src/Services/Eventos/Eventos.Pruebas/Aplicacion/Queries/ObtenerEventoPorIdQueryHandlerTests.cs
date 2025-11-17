using Eventos.Aplicacion.Queries;
using Eventos.Dominio.Entidades;
using Eventos.Dominio.Enumeraciones;
using Eventos.Dominio.Repositorios;
using Eventos.Dominio.ObjetosDeValor;
using FluentAssertions;
using Moq;
using Xunit;
using System;

namespace Eventos.Pruebas.Aplicacion.Queries;

public class ObtenerEventoPorIdQueryHandlerTests
{
    private readonly Mock<IRepositorioEvento> _repositoryMock;
    private readonly ObtenerEventoPorIdQueryHandler _handler;
    private Evento CrearEventoBase() => new("ArtCraft", "Evento de arte", new Ubicacion("Av Principal123","Sucre","Caracas","DF","1029","Venezuela"), DateTime.UtcNow.AddMonths(1), DateTime.UtcNow.AddMonths(1).AddHours(8),100, "organizador-001");

    public ObtenerEventoPorIdQueryHandlerTests()
    {
        _repositoryMock = new Mock<IRepositorioEvento>();
        _handler = new ObtenerEventoPorIdQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_DeberiaDevolverEventoDto_CuandoEventoExiste()
    {
        // Preparar
        var evento = CrearEventoBase();
        _repositoryMock.Setup(x => x.ObtenerPorIdAsync(evento.Id, It.IsAny<CancellationToken>())).ReturnsAsync(evento);

        var result = await _handler.Handle(new ObtenerEventoPorIdQuery(evento.Id), CancellationToken.None);

        // Assert
        result.EsExitoso.Should().BeTrue();
        result.Valor!.Id.Should().Be(evento.Id);
        result.Valor.Estado.Should().Be(EstadoEvento.Borrador.ToString());
    }

    [Fact]
    public async Task Handle_DeberiaDevolverFalla_CuandoEventoNoExiste()
    {
        // Preparar
        _repositoryMock.Setup(x => x.ObtenerPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync((Evento?)null);

        // Act
        var result = await _handler.Handle(new ObtenerEventoPorIdQuery(Guid.NewGuid()), CancellationToken.None);

        // Assert
        result.EsExitoso.Should().BeFalse();
        result.Error.Should().Be("Evento no encontrado");
    }

    [Fact]
    public async Task Handle_DeberiaMapearUbicacion_CuandoEventoTieneUbicacion()
    {
        // Preparar
        var evento = CrearEventoBase();
        _repositoryMock.Setup(x => x.ObtenerPorIdAsync(evento.Id, It.IsAny<CancellationToken>())).ReturnsAsync(evento);

        // Act
        var result = await _handler.Handle(new ObtenerEventoPorIdQuery(evento.Id), CancellationToken.None);

        // Assert
        var dto = result.Valor!;
        dto.Ubicacion!.NombreLugar.Should().Be("Av Principal123");
        dto.Ubicacion.Direccion.Should().Be("Sucre");
    }

    [Fact]
    public async Task Handle_DeberiaPropagarOperacionCancelada_CuandoTokenCancelado()
    {
        // Preparar
        var cts = new CancellationTokenSource();
        cts.Cancel();
        _repositoryMock.Setup(x => x.ObtenerPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ThrowsAsync(new OperationCanceledException());

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(() => _handler.Handle(new ObtenerEventoPorIdQuery(Guid.NewGuid()), cts.Token));
    }

    [Fact]
    public async Task Handle_DeberiaIncluirAsistentes_CuandoEventoTieneAsistentes()
    {
        // Preparar
        var evento = CrearEventoBase();
        evento.Publicar();
        evento.RegistrarAsistente("usuario-001", "Creonte Lara", "cdlara@est.ucab.edu.ve");
        evento.RegistrarAsistente("usuario-002", "Electra Wilson", "eywilson@est.ucab.edu.ve");
        _repositoryMock.Setup(x => x.ObtenerPorIdAsync(evento.Id, It.IsAny<CancellationToken>())).ReturnsAsync(evento);

        // Act
        var result = await _handler.Handle(new ObtenerEventoPorIdQuery(evento.Id), CancellationToken.None);

        // Assert
        result.Valor!.Asistentes.Should().HaveCount(2);
    }

    [Fact]
    public async Task Handle_EventoConAsistentes_MapeaLista()
    {
        // Preparar
        var evento = CrearEventoBase();
        evento.Publicar();
        evento.RegistrarAsistente("u1","Nombre1","a@b.com");
        evento.RegistrarAsistente("u2","Nombre2","c@d.com");
        _repositoryMock.Setup(r => r.ObtenerPorIdAsync(evento.Id, It.IsAny<CancellationToken>())).ReturnsAsync(evento);

        // Act
        var res = await _handler.Handle(new ObtenerEventoPorIdQuery(evento.Id), CancellationToken.None);

        // Assert
        res.EsExitoso.Should().BeTrue();
        res.Valor!.ConteoAsistentesActual.Should().Be(2);
    }
}
