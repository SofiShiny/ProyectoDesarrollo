using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Eventos.Dominio.Entidades;
using Eventos.Dominio.Enumeraciones;

namespace Events.Infrastructure.Persistence.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Evento>
{
    public void Configure(EntityTypeBuilder<Evento> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Titulo)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Descripcion)
            .IsRequired()
            .HasMaxLength(2000);

        builder.OwnsOne(e => e.Ubicacion, location =>
        {
            location.Property(l => l.NombreLugar).HasMaxLength(200).IsRequired();
            location.Property(l => l.Direccion).HasMaxLength(300).IsRequired();
            location.Property(l => l.Ciudad).HasMaxLength(100).IsRequired();
            location.Property(l => l.Region).HasMaxLength(100);
            location.Property(l => l.CodigoPostal).HasMaxLength(20);
            location.Property(l => l.Pais).HasMaxLength(100).IsRequired();
        });

        builder.Property(e => e.FechaInicio).IsRequired();
        builder.Property(e => e.FechaFin).IsRequired();
        builder.Property(e => e.MaximoAsistentes).IsRequired();
        
        builder.Property(e => e.Estado)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(e => e.OrganizadorId)
            .IsRequired()
            .HasMaxLength(100);

        builder.Ignore(e => e.DomainEvents);

        builder.HasMany(e => e.Asistentes)
            .WithOne()
            .HasForeignKey(a => a.EventoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => e.FechaInicio);
        builder.HasIndex(e => e.Estado);
        builder.HasIndex(e => e.OrganizadorId);
    }
}
