using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Zoo.Application.Core.Behaviours;

namespace Zoo.Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(c =>
            {
                c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                c.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>), ServiceLifetime.Scoped);
            });

            return services;
        }
    }
}
