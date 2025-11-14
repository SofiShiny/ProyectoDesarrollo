using Eventos.Aplicacion.DTOs;
using Eventos.Aplicacion.Validators;
using FluentAssertions;
using System;
using Xunit;

namespace Eventos.Pruebas.Aplicacion.Validators
{
 public class EventoUpdateValidatorTests
 {
 [Fact]
 public void CuandoAmbasFechas_EstanProveidas_OrdenInvalido_DeberiaTenerError()
 {
 // Preparar
 var dto = new EventoUpdateDto
 {
 FechaInicio = DateTime.UtcNow.AddDays(2),
 FechaFin = DateTime.UtcNow.AddDays(1)
 };

 var validator = new EventoUpdateValidator();
 
 // Actuar
 var result = validator.Validate(dto);

 // Comprobar
 result.IsValid.Should().BeFalse();
 result.Errors.Should().Contain(e => e.ErrorMessage.Contains("FechaInicio") || e.PropertyName.Contains("FechaInicio"));
 }

 [Fact]
 public void CuandoMaximoEsProveido_ValorInvalido_DeberiaTenerError()
 {
 // Preparar
 var dto = new EventoUpdateDto
 {
 MaximoAsistentes =0
 };

 var validator = new EventoUpdateValidator();
 
 // Actuar
 var result = validator.Validate(dto);

 // Comprobar
 result.IsValid.Should().BeFalse();
 result.Errors.Should().Contain(e => e.PropertyName == "MaximoAsistentes");
 }
 }
}
