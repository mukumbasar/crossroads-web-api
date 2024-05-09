using Crossroads.Application.Dtos.Configurations;
using Crossroads.Application.Interfaces;
using Crossroads.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceExtensions(this IServiceCollection services)
        {
            services.AddScoped<IJWTService, JWTService>();

            return services;
        }

        public static IServiceCollection AddIdentityExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWTOption>(configuration.GetSection("TokenOptions"));
            var jwtOptions = configuration.GetSection("TokenOptions").Get<JWTOption>();
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
            {
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience[0],
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = SignService.GetSymmetricSecurityKey(jwtOptions.SecurityKey),
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}
