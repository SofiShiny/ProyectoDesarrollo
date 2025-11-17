using Eventos.Aplicacion.Comandos;
using Eventos.Aplicacion.DTOs;
using Eventos.Dominio.Entidades;
using Eventos.Dominio.Repositorios;
using Eventos.Dominio.ObjetosDeValor;
using FluentAssertions;
using Moq;
using Xunit;
using System;

namespace Eventos.Pruebas.Aplicacion.Comandos;

public class CrearEventoComandoHandlerTests
{
 private readonly Mock<IRepositorioEvento> _repositorioEventoMock;
 private readonly CrearEventoComandoHandler _handler;
 private UbicacionDto UbicacionValida => new() { NombreLugar = "Lugar", Direccion = "Dir", Ciudad = "Ciudad", Region = "Reg", CodigoPostal = "0000", Pais = "Pais" };

 public CrearEventoComandoHandlerTests()
 {
 _repositorioEventoMock = new Mock<IRepositorioEvento>();
 _handler = new CrearEventoComandoHandler(_repositorioEventoMock.Object);
 }

 [Fact]
 public async Task Handle_ConComandoValido_DeberiaCrearEventoYDevolverExito()
 {
 var comando = new CrearEventoComando("Conferencia", "Desc", UbicacionValida, DateTime.UtcNow.AddDays(5), DateTime.UtcNow.AddDays(6),100, "org-001");
 var result = await _handler.Handle(comando, CancellationToken.None);
 result.IsExito.Should().BeTrue();
 result.Value!.Titulo.Should().Be(comando.Titulo);
 _repositorioEventoMock.Verify(x => x.AgregarAsync(It.IsAny<Evento>(), It.IsAny<CancellationToken>()), Times.Once);
 }

 [Fact]
 public async Task Handle_ConUbicacionNula_DeberiaDevolverFalla()
 {
 var comando = new CrearEventoComando("Conferencia", "Desc", null!, DateTime.UtcNow.AddDays(5), DateTime.UtcNow.AddDays(6),100, "org-001");
 var result = await _handler.Handle(comando, CancellationToken.None);
 result.IsExito.Should().BeFalse();
 result.Error.Should().Contain("ubicacion");
 }

 [Fact]
 public async Task Handle_FechasInvalidas_RetornaFalla()
 {
 var inicio = DateTime.UtcNow.AddDays(2);
 var comando = new CrearEventoComando("Conferencia", "Desc", UbicacionValida, inicio, inicio,100, "org-001");
 var result = await _handler.Handle(comando, CancellationToken.None);
 result.IsExito.Should().BeFalse();
 result.Error.Should().Contain("fecha fin");
 }

 [Fact]
 public async Task Handle_MaximoAsistentesInvalido_RetornaFalla()
 {
 var comando = new CrearEventoComando("Conferencia", "Desc", UbicacionValida, DateTime.UtcNow.AddDays(2), DateTime.UtcNow.AddDays(3),0, "org-001");
 var result = await _handler.Handle(comando, CancellationToken.None);
 result.IsExito.Should().BeFalse();
 result.Error.Should().Contain("maximo");
 }
}