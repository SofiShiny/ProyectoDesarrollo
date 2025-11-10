using Eventos.Aplicacion.Comandos;
using Eventos.Aplicacion.DTOs;
using Eventos.Dominio.Entidades;
using Eventos.Dominio.Repositorios;
using Eventos.Dominio.ObjetosDeValor;
using FluentAssertions;
using Moq;
using Xunit;

namespace Eventos.Tests.Application.Comandos;

public class CrearEventoComandoHandlerTests
{
    private readonly Mock<IRepositorioEvento> _repositorioEventoMock;
    private readonly CrearEventoComandoHandler _handler;

    public CrearEventoComandoHandlerTests()
    {
        _repositorioEventoMock = new Mock<IRepositorioEvento>();
        _handler = new CrearEventoComandoHandler(_repositorioEventoMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidComando_ShouldCreateEventAndReturnSuccess()
    {
        // Arrange
        var ubicacionDto = new UbicacionDto
        {
            NombreLugar = "Main Hall",
            Direccion = "123 St",
            Ciudad = "NY",
            Region = "NY",
            CodigoPostal = "10001",
            Pais = "USA"
        }!;

        var comando = new CrearEventoComando(
            "TechConf",
            "Tech conference",
            ubicacionDto,
            DateTime.UtcNow.AddDays(5),
            DateTime.UtcNow.AddDays(6),
            100,
            "org-001"
        );

        var expectedEvento = new Evento(
            comando.Titulo,
            comando.Descripcion,
            new Ubicacion(
                ubicacionDto.NombreLugar,
                ubicacionDto.Direccion,
                ubicacionDto.Ciudad,
                ubicacionDto.Region,
                ubicacionDto.CodigoPostal,
                ubicacionDto.Pais
            ),
            comando.FechaInicio,
            comando.FechaFin,
            comando.MaximoAsistentes,
            comando.OrganizadorId
        );

        // El repositorio agrega el evento y no devuelve valor
        _repositorioEventoMock
            .Setup(x => x.AgregarAsync(It.IsAny<Evento>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(comando, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Titulo.Should().Be(comando.Titulo);
        result.Value.Descripcion.Should().Be(comando.Descripcion);
        result.Value.Ubicacion.Should().NotBeNull();
        result.Value.Ubicacion.Ciudad.Should().Be("NY");

        _repositorioEventoMock.Verify(x => x.AgregarAsync(It.IsAny<Evento>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNullLocation_ShouldReturnFailure()
    {
        // Arrange
        var comando = new CrearEventoComando(
            "TechConf",
            "Tech conference",
            null!,
            DateTime.UtcNow.AddDays(5),
            DateTime.UtcNow.AddDays(6),
            100,
            "org-001"
        );

        // Act
        var result = await _handler.Handle(comando, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("La ubicación es obligatoria");
        _repositorioEventoMock.Verify(x => x.AgregarAsync(It.IsAny<Evento>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenRepositoryThrowsArgumentException_ShouldReturnFailure()
    {
        // Arrange
        var ubicacionDto = new UbicacionDto
        {
            NombreLugar = "Main Hall",
            Direccion = "123 St",
            Ciudad = "NY",
            Region = "NY",
            CodigoPostal = "10001",
            Pais = "USA"
        }!;

        var comando = new CrearEventoComando(
            "TechConf",
            "Tech conference",
            ubicacionDto,
            DateTime.UtcNow.AddDays(5),
            DateTime.UtcNow.AddDays(6),
            100,
            "org-001"
        );

        _repositorioEventoMock
            .Setup(x => x.AgregarAsync(It.IsAny<Evento>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new ArgumentException("La fecha de inicio debe ser en el futuro"));

        // Act
        var result = await _handler.Handle(comando, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("La fecha de inicio debe ser en el futuro");
    }
}