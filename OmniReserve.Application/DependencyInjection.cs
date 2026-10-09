using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OmniReserve.Application.Common.Behaviors;

namespace OmniReserve.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        // Obtener el Assembly de Application
        var assembly = Assembly.GetExecutingAssembly();

        // Registrar automáticamente los validadores
        services.AddValidatorsFromAssembly(assembly);

        // Registrar MediatR y el ValidationBehavior
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);

            cfg.AddBehavior(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>)
            );
        });

        return services;
    }
}