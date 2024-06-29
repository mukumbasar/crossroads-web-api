using Crossroads.Persistence.Context;
using Crossroads.Persistence.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Persistence.Extensions
{
    public static class CrossroadsDbContextExtensions
    {
        public static IServiceCollection AddCrossroadsDbContextConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure DbContext with lazy loading and SQL Server for run-time
            services.AddDbContext<CrossroadsDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(configuration.GetConnectionString("MainConnection"));
            });

            // Configure Identity
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<CrossroadsDbContext>()
            .AddDefaultTokenProviders();

            AdminSeed.SeedAsync(configuration).GetAwaiter().GetResult();

            return services;
        }
    }
}
