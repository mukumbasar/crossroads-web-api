using Crossroads.Domain.DataAccess.Interfaces;
using Crossroads.Domain.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Interfaces.Repositories
{
    public interface IChatRoomChatterRepository :
    IAsyncFindableRepository<ChatRoomChatter>,
    IAsyncInsertableRepository<ChatRoomChatter>,
    IAsyncUpdateableRepository<ChatRoomChatter>,
    IAsyncDeleteableRepository<ChatRoomChatter>,
    IAsyncTransactionRepository,
    IRepository
    {

    }
}
