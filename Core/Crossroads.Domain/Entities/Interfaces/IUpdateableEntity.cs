using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Domain.Entities.Interfaces
{
    public interface IUpdateableEntity : IEntity
    {
        string ModifiedBy { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}
