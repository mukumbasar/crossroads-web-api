using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Dtos.Email
{
    public class EmailContext
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
