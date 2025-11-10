using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Eventos.Dominio.Entidades;

namespace Eventos.Infraestructura.Persistence.Configurations;

public class AttendeeConfiguration : IEntityTypeConfiguration<Asistente>
{
    public void Configure(EntityTypeBuilder<Asistente> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.UsuarioId)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.NombreUsuario)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.Correo)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(a => a.RegistradoEn).IsRequired();


        builder.HasIndex(a => new { a.EventoId, a.UsuarioId }).IsUnique();
    }
}
