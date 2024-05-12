using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace Crossroads.WebAPI.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IServiceCollection AddFluentValidationWithAssemblies(this IServiceCollection services)
        {

            services.AddFluentValidationAutoValidation(config => config.DisableDataAnnotationsValidation = true) 
                    .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
