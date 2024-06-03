using Crossroads.Domain.DataAccess.Interfaces;
using Crossroads.Domain.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Interfaces.Repositories
{
    public interface ICategoryRepository :
    IAsyncFindableRepository<Category>,
    IAsyncInsertableRepository<Category>,
    IAsyncUpdateableRepository<Category>,
    IAsyncDeleteableRepository<Category>,
    IAsyncTransactionRepository,
    IRepository
    {

    }
}
