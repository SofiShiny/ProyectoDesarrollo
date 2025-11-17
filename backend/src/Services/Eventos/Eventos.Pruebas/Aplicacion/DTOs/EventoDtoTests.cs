using Eventos.Aplicacion.DTOs;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Eventos.Pruebas.Aplicacion.DTOs;

public class EventoDtoPruebas
{
    [Fact]
    public void EventoDto_DebeInicializarTodasLasPropiedades()
    {
        // Preparar
        var id = Guid.NewGuid();
        var titulo = "Concierto musica";
        var descripcion = "Concierto de musica navideña";
        var fechaInicio = DateTime.UtcNow.AddDays(30);
        var fechaFin = fechaInicio.AddHours(8);
        var maximoAsistentes =100;
        var estado = "Publicado";

        // Ejecutar
        var dto = new EventoDto
        {
            Id = id,
            Titulo = titulo,
            Descripcion = descripcion,
            FechaInicio = fechaInicio,
            FechaFin = fechaFin,
            MaximoAsistentes = maximoAsistentes,
            Estado = estado
        };

        // Comprobar
        dto.Id.Should().Be(id);
        dto.Titulo.Should().Be(titulo);
        dto.Descripcion.Should().Be(descripcion);
        dto.FechaInicio.Should().Be(fechaInicio);
        dto.FechaFin.Should().Be(fechaFin);
        dto.MaximoAsistentes.Should().Be(maximoAsistentes);
        dto.Estado.Should().Be(estado);
    }

    [Fact]
    public void EventoDto_Ubicacion_DebeSerAsignable()
    {
        // Preparar
        var dto = new EventoDto();
        var ubicacionDto = new UbicacionDto
        {
            NombreLugar = "CCCT",
            Direccion = "Av la Estancia",
            Ciudad = "Caracas",
            Region = "DF",
            CodigoPostal = "1090",
            Pais = "Venezuela"
        };

        // Ejecutar
        dto.Ubicacion = ubicacionDto;

        // Comprobar
        dto.Ubicacion.Should().NotBeNull();
        dto.Ubicacion!.NombreLugar.Should().Be("CCCT");
    }

    [Fact]
    public void EventoDto_Asistentes_DebeSerAsignableYEvaluable()
    {
        // Preparar
        var dto = new EventoDto();
        var asistentes = new List<AsistenteDto>
        {
            new() { Id = Guid.NewGuid(), NombreUsuario = "Creonte Lara", Correo = "cdlara@est.ucab.edu.ve" },
            new() { Id = Guid.NewGuid(), NombreUsuario = "Electra Wilson", Correo = "eywilson@est.ucab.edu.ve" }
        };

        // Ejecutar
        dto.Asistentes = asistentes;

        // Comprobar
        dto.Asistentes.Should().HaveCount(2);
        dto.Asistentes.Should().Contain(a => a.NombreUsuario == "Creonte Lara");
    }

    [Fact]
    public void EventoDto_DebePermitirValoresNulos()
    {
        // Preparar y ejecutar
        var dto = new EventoDto
        {
            Descripcion = null,
            Ubicacion = null,
            Asistentes = null
        };

        // Comprobar
        dto.Descripcion.Should().BeNull();
        dto.Ubicacion.Should().BeNull();
        dto.Asistentes.Should().BeNull();
    }

    [Fact]
    public void EventoDto_Defaults_IdVacioYTituloNulo()
    {
        // Preparar y ejecutar
        var dto = new EventoDto();

        // Comprobar
        dto.Id.Should().Be(Guid.Empty);
        dto.Titulo.Should().BeNull();
        dto.Asistentes.Should().BeNull();
    }
}
