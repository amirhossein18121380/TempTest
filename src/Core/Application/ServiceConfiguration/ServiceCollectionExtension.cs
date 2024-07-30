using System.Reflection;
using Application.Common;
using Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace Application.ServiceConfiguration;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
            options.Namespace = "CleanArc.Application.Mediator";
        });
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidateCommandBehavior<,>));
        //services.AddMediator(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(expression =>
        {
            expression.AddMaps(Assembly.GetExecutingAssembly());
        });


        return services;
    }

   
}