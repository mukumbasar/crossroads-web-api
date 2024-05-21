using Crossroads.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Domain.Entities.Bases
{
    public class BaseUser : AuditableEntity
    {
        public string? IdentityId { get; set; }
    }
}
