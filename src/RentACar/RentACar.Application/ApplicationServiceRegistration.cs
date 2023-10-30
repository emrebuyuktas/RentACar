using Core.Application.Piplines.Caching;
using Core.Application.Piplines.Transaction;
using Core.Application.Piplines.Validation;
using Core.Application.Rules;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace RentACar.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(cofiguration =>
        {
            cofiguration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            cofiguration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));

            cofiguration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));

            cofiguration.AddOpenBehavior(typeof(CachingBehavior<,>));

            cofiguration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
        });

        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(),typeof(BaseBusinessRules));

        return services;
    }

    public static IServiceCollection AddSubClassesOfType(this IServiceCollection services, Assembly assembly, 
        Type type, Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null)
    {
        var types = assembly.GetTypes()
            .Where(t => t.IsSubclassOf(type) && type != t).ToList();

        foreach (var item in types)
        {
            if(addWithLifeCycle is null)
            {
                services.AddScoped(item);
            }
            else
            {
                addWithLifeCycle(services, type);
            }
        }
        return services;
    }
}