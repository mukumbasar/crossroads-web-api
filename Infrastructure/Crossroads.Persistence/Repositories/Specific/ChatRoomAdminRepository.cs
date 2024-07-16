﻿using Crossroads.Application.Interfaces.Repositories;
using Crossroads.Domain.Entities.DbSets;
using Crossroads.Persistence.Context;
using Crossroads.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Persistence.Repositories.Specific
{
    public class ChatRoomAdminRepository : EFBaseRepository<ChatRoomAdmin>, IChatRoomAdminRepository
    {
        public ChatRoomAdminRepository(CrossroadsDbContext context) : base(context)
        {

        }
    }
}
