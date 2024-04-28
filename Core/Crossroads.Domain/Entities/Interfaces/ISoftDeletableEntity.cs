using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Domain.Entities.Interfaces
{
    public interface ISoftDeletableEntity : IEntity
    {
        string? DeletedBy { get; set; }
        DateTime? DeletedDate { get; set; }
    }
}
