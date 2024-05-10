using Crossroads.Domain.Entities.DbSets;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(IdentityAppUser user);
        Task<string> GenerateRefreshToken(IdentityAppUser user);
        Task<string> GenerateExpiredAccessToken();
        
    }
}
