namespace Eventos.Aplicacion.DTOs;

public class AsistenteResponseDto
{
 public Guid Id { get; set; }
 public string Nombre { get; set; } = string.Empty;
 public string? Correo { get; set; }
 public DateTime RegistradoEn { get; set; }
}
