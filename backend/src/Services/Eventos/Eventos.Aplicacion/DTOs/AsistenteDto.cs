namespace Eventos.Aplicacion.DTOs;

public class AsistenteDto
{
    public Guid Id { get; set; }
    public string? NombreUsuario { get; set; }
    public string? Nombre { get; set; }
    public string? Correo { get; set; }
    public string? Email { get => Correo; set => Correo = value; }
    public DateTime RegistradoEn { get; set; }
}
