using Crossroads.Application.Dtos.Configurations;
using Crossroads.Application.Interfaces.Services;
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
    public static class JwtExtensions
    {
        public static IServiceCollection AddJwtConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            // Make JWTOption available throughout the application via dependency injection.
            services.Configure<TokenOptions>(configuration.GetSection("TokenOptions"));
            // Retrieve JWTOption to be used.
            var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
            // Set up authentication schemes.
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                // Configure JWT Bearer authentication.
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                {
                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience[0],
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }
    }
}
