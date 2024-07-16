using Crossroads.Domain.DataAccess.Interfaces;
using Crossroads.Domain.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Interfaces.Repositories
{
    public interface IChatRoomAdminRepository :
    IAsyncFindableRepository<ChatRoomAdmin>,
    IAsyncInsertableRepository<ChatRoomAdmin>,
    IAsyncUpdateableRepository<ChatRoomAdmin>,
    IAsyncDeleteableRepository<ChatRoomAdmin>,
    IAsyncTransactionRepository,
    IRepository
    {

    }
}
