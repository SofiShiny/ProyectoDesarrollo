using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Eventos.Dominio.Repositorios;
using Eventos.Infraestructura.Persistencia;
using Eventos.Infraestructura.Repositorios;

namespace Eventos.Infraestructura;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventoInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("EventosDb") 
            ?? throw new InvalidOperationException("EventosDb connection string is not configured");

        services.AddDbContext<EventosDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IRepositorioEvento, EventoRepository>();

        return services;
    }
}
