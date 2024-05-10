using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Dtos.Configurations
{
    public class TokenOptions
    {
        public List<string> Audience { get; set; }
        public string Issuer { get; set; }
        public int JWTAccessTokenExpirationTime { get; set; }
        public int RefreshTokenExpirationTime { get; set; }
        public string SecurityKey { get; set; }
    }
}
