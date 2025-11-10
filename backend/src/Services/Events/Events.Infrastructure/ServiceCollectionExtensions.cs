using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Eventos.Dominio.Repositorios;
using Eventos.Infraestructura.Persistence;
using Eventos.Infraestructura.Repositorios;

namespace Eventos.Infraestructura;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("EventsDb") 
            ?? throw new InvalidOperationException("EventsDb connection string is not configured");

        services.AddDbContext<EventsDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IRepositorioEvento, EventRepository>();

        return services;
    }
}
