using Crossroads.Application.Interfaces.Repositories;
using Crossroads.Domain.Entities.DbSets;
using Crossroads.Persistence.Repositories.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Crossroads.Persistence.Repositories.Specific
{
    public class RefreshTokenRepository : EFBaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(IdentityDbContext<IdentityUser, IdentityRole, string> context) : base(context)
        {

        }
    }
}
