using Eventos.Aplicacion.DTOs;
using Eventos.Aplicacion.Validators;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Eventos.Pruebas.Aplicacion.Validators
{
 public class EventoCreateValidatorTests
 {
 [Fact]
 public void DatosValidos_DeberiaSerValido()
 {
 // Preparar
 var dto = new EventoCreateDto
 {
 Titulo = "Taller de Arte",
 FechaInicio = DateTime.UtcNow.AddDays(1),
 FechaFin = DateTime.UtcNow.AddDays(1).AddHours(2),
 MaximoAsistentes =10,
 Ubicacion = new UbicacionDto{ NombreLugar="L", Direccion="D", Ciudad="C", Pais="P"},
 Asistentes = new List<AsistenteCreateDto>
 {
 new AsistenteCreateDto { Nombre = "Creonte Dioniso Lara Wilson", Correo = "cdlara@est.ucab.edu.ve" }
 }
 };

 var validator = new EventoCreateValidator();
 
 // Actuar
 var result = validator.Validate(dto);

 // Comprobar
 result.IsValid.Should().BeTrue();
 }

 [Fact]
 public void TituloFaltante_DeberiaTenerError()
 {
 // Preparar
 var dto = new EventoCreateDto
 {
 Titulo = string.Empty,
 FechaInicio = DateTime.UtcNow.AddDays(1),
 FechaFin = DateTime.UtcNow.AddDays(1).AddHours(2),
 MaximoAsistentes =5,
 Ubicacion = new UbicacionDto{ NombreLugar="L", Direccion="D", Ciudad="C", Pais="P"}
 };

 var validator = new EventoCreateValidator();
 
 // Actuar
 var result = validator.Validate(dto);

 // Comprobar
 result.IsValid.Should().BeFalse();
 result.Errors.Should().Contain(e => e.PropertyName == "Titulo");
 }

 [Fact]
 public void FechaInicioMayorQueFechaFin_DeberiaTenerError()
 {
 // Preparar
 var dto = new EventoCreateDto
 {
 Titulo = "Taller de Arte",
 FechaInicio = DateTime.UtcNow.AddDays(2),
 FechaFin = DateTime.UtcNow.AddDays(1),
 MaximoAsistentes =5,
 Ubicacion = new UbicacionDto{ NombreLugar="L", Direccion="D", Ciudad="C", Pais="P"}
 };

 var validator = new EventoCreateValidator();
 
 // Actuar
 var result = validator.Validate(dto);

 // Comprobar
 result.IsValid.Should().BeFalse();
 }

 [Fact]
 public void MaximoAsistentesInvalido_DeberiaTenerError()
 {
 // Preparar
 var dto = new EventoCreateDto
 {
 Titulo = "Taller de Arte",
 FechaInicio = DateTime.UtcNow.AddDays(1),
 FechaFin = DateTime.UtcNow.AddDays(1).AddHours(1),
 MaximoAsistentes =0,
 Ubicacion = new UbicacionDto{ NombreLugar="L", Direccion="D", Ciudad="C", Pais="P"}
 };

 var validator = new EventoCreateValidator();
 
 // Actuar
 var result = validator.Validate(dto);

 // Comprobar
 result.IsValid.Should().BeFalse();
 result.Errors.Should().Contain(e => e.PropertyName == "MaximoAsistentes");
 }

 [Fact]
 public void EmailDeAsistenteInvalido_DeberiaTenerError()
 {
 // Preparar
 var dto = new EventoCreateDto
 {
 Titulo = "Taller de Arte",
 FechaInicio = DateTime.UtcNow.AddDays(1),
 FechaFin = DateTime.UtcNow.AddDays(1).AddHours(1),
 MaximoAsistentes =5,
 Ubicacion = new UbicacionDto{ NombreLugar="L", Direccion="D", Ciudad="C", Pais="P"},
 Asistentes = new List<AsistenteCreateDto>
 {
 new AsistenteCreateDto { Nombre = "Creonte Dioniso Lara Wilson", Correo = "not-an-email" }
 }
 };

 var validator = new EventoCreateValidator();
 
 // Actuar
 var result = validator.Validate(dto);

 // Comprobar
 result.IsValid.Should().BeFalse();
 }

 [Fact]
 public void MaximoAsistentesUno_Valido()
 {
 // Preparar
 var dto = new EventoCreateDto
 {
 Titulo = "T",
 FechaInicio = DateTime.UtcNow.AddDays(1),
 FechaFin = DateTime.UtcNow.AddDays(2),
 MaximoAsistentes =1,
 Ubicacion = new UbicacionDto{ NombreLugar="L", Direccion="D", Ciudad="C", Pais="P"}
 };

 // Comprobar
 new EventoCreateValidator().Validate(dto).IsValid.Should().BeTrue();
 }

 [Fact]
 public void FechasIguales_Invalido()
 {
 // Preparar
 var inicio = DateTime.UtcNow.AddDays(2);
 var dto = new EventoCreateDto
 {
 Titulo = "T",
 FechaInicio = inicio,
 FechaFin = inicio,
 MaximoAsistentes =5,
 Ubicacion = new UbicacionDto{ NombreLugar="L", Direccion="D", Ciudad="C", Pais="P"}
 };

 // Comprobar
 new EventoCreateValidator().Validate(dto).IsValid.Should().BeFalse();
 }
 }
}
