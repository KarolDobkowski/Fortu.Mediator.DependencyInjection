using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Fortu.Mediator.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();
            return services;
        }

        public static IServiceCollection RegisterHandler<THandler>(this IServiceCollection services)
        {
            var typeInterfaces = typeof(THandler)
                .GetInterfaces()
                .Where(x => x.GetGenericTypeDefinition() == typeof(IMessageHandler<>) ||
                            x.GetGenericTypeDefinition() == typeof(IMessageHandler<,>))
                .ToArray();

            if (!typeInterfaces.Any())
                throw new InvalidOperationException("'THandler' is not implementation of any IMessageHandler.");

            foreach (var typeInterface in typeInterfaces)
            {
                var genericTypeDefinition = typeInterface.GetGenericTypeDefinition();

                if (genericTypeDefinition == typeof(IMessageHandler<>))
                    services.AddTransient(
                        typeof(IMessageHandler<>).MakeGenericType(typeInterface.GetGenericArguments()[0]),
                        typeof(THandler));

                else if (genericTypeDefinition == typeof(IMessageHandler<,>))
                    services.AddTransient(
                        typeof(IMessageHandler<,>).MakeGenericType(
                            typeInterface.GetGenericArguments()[0],
                            typeInterface.GetGenericArguments()[1]),
                        typeof(THandler));
            }

            return services;
        }
    }
}
