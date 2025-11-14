using Eventos.Aplicacion.DTOs;
using Eventos.Aplicacion.Validators;
using FluentAssertions;
using Xunit;

namespace Eventos.Pruebas.Aplicacion.Validators
{
 public class AsistenteCreateValidatorTests
 {
 [Fact]
 public void AsistenteValido_DeberiaSerValido()
 {
 // Preparar
 var dto = new AsistenteCreateDto { Nombre = "Creonte Dioniso Lara Wilson", Correo = "cdlara@est.ucab.edu.ve" };
 var validator = new AsistenteCreateValidator();
 
 // Actuar
 var result = validator.Validate(dto);
 
 // Comprobar
 result.IsValid.Should().BeTrue();
 }

 [Fact]
 public void EmailInvalido_DeberiaTenerError()
 {
 // Preparar
 var dto = new AsistenteCreateDto { Nombre = "Creonte Dioniso Lara Wilson", Correo = "invalid-email" };
 var validator = new AsistenteCreateValidator();
 
 // Actuar
 var result = validator.Validate(dto);
 
 // Comprobar
 result.IsValid.Should().BeFalse();
 result.Errors.Should().Contain(e => e.PropertyName == "Correo");
 }
 }
}
