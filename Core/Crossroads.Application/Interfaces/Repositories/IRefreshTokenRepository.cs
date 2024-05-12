using Crossroads.Domain.DataAccess.Interfaces;
using Crossroads.Domain.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Crossroads.Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepository :
    IAsyncFindableRepository<RefreshToken>,
    IAsyncInsertableRepository<RefreshToken>,
    IAsyncUpdateableRepository<RefreshToken>,
    IAsyncDeleteableRepository<RefreshToken>,
    IRepository
    {

    }
}
