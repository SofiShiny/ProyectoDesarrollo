using Eventos.Aplicacion.DTOs;
using FluentAssertions;
using Xunit;
using System;

namespace Eventos.Pruebas.Aplicacion.DTOs;

public class UbicacionDtoTests
{
    [Fact]
    public void UbicacionDto_DebeInicializarTodasLasPropiedades()
    {
        // Preparar & Ejecutar
        var dto = new UbicacionDto
        {
            NombreLugar = "Centro de Convenciones",
            Direccion = "Av Principal123",
            Ciudad = "Caracas",
            Region = "Distrito Capital",
            CodigoPostal = "1010",
            Pais = "Venezuela"
        };

        // Comprobar
        dto.NombreLugar.Should().Be("Centro de Convenciones");
        dto.Direccion.Should().Be("Av Principal123");
    }

    [Fact]
    public void UbicacionDto_DebePermitirCadenasVacias()
    {
        // Preparar & Ejecutar
        var dto = new UbicacionDto
        {
            NombreLugar = string.Empty,
            Direccion = string.Empty,
            Ciudad = string.Empty,
            Region = string.Empty,
            CodigoPostal = string.Empty,
            Pais = string.Empty
        };

        // Comprobar
        dto.NombreLugar.Should().BeEmpty();
        dto.Pais.Should().BeEmpty();
    }

    [Fact]
    public void UbicacionDto_Defaults_Nulos()
    {
        var dto = new UbicacionDto();
        dto.NombreLugar.Should().BeNull();
        dto.Pais.Should().BeNull();
    }
}
