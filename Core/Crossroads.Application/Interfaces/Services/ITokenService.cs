using Crossroads.Domain.Entities.DbSets;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Interfaces.Services
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(IdentityUser user);
        Task<string> GenerateRefreshToken(IdentityUser user);
        Task<string> GenerateExpiredAccessToken();

    }
}
