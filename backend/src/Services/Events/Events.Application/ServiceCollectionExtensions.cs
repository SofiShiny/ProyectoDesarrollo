using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;

namespace Events.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        return services;
    }
}
