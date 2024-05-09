using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Interfaces
{
    public interface IJWTService
    {
        Task<string> CreateToken(IdentityUser user);
        Task<string> RevokeTokenWithExpiretime();
    }
}
