using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Dtos.Configurations
{
    public class JWTOption
    {
        public List<string> Audience { get; set; }
        public string Issuer { get; set; }
        public int JWTExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
