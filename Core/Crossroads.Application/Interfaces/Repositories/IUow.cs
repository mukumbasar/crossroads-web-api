using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Interfaces.Repositories
{
    public interface IUow
    {
        Task CommitAsync();
        Task<IAppUserRepository> GetAppUserRepositoryAsync();
        Task<IRefreshTokenRepository> GetRefreshTokenRepositoryAsync();
        Task<ICategoryRepository> GetCategoryRepositoryAsync();
    }
}
