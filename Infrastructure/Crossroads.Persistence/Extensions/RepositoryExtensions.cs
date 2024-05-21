using Crossroads.Application.Interfaces.Repositories;
using Crossroads.Application.Interfaces.Services;
using Crossroads.Application.Services;
using Crossroads.Persistence.Repositories.Specific;
using Crossroads.Persistence.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Persistence.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();

            services.AddScoped<IUow, Uow>();

            return services;
        }
    }
}
