using Crossroads.Application.Dtos.Configurations;
using Crossroads.Application.Features.AppUser.Commands.AddAppUser;
using Crossroads.Application.Interfaces.Services;
using Crossroads.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Extensions
{
    public static class FeatureExtensions
    {
        public static IServiceCollection AddFeatureExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            //services.AddSingleton<IRedisService>(provider =>
            //{
            //    return new RedisService(configuration.GetSection("Redis")["ConnectionString"]);
            //});

            services.AddScoped<IImageConversionService, ImageConversionService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}
