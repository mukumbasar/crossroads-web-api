using Crossroads.Application.Interfaces.Repositories;
using Crossroads.Domain.Entities.DbSets;
using Crossroads.Persistence.Context;
using Crossroads.Persistence.Repositories.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Persistence.Repositories.Specific
{
    public class AppUserRepository : EFBaseRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(CrossroadsDbContext context) : base(context)
        {

        }
    }
}
