using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;

namespace Eventos.Aplicacion;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventAplicacionServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        return services;
    }
}
