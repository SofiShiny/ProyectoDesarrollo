namespace Eventos.Aplicacion.DTOs;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class EventoCreateDto
{
    [Required]
    public string Titulo { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    public UbicacionDto? Ubicacion { get; set; }

    [Required]
    public DateTime FechaInicio { get; set; }

    [Required]
    public DateTime FechaFin { get; set; }

    [Range(1, int.MaxValue)]
    public int MaximoAsistentes { get; set; } = 1;

    public string? Estado { get; set; }

    public List<AsistenteCreateDto>? Asistentes { get; set; }
}