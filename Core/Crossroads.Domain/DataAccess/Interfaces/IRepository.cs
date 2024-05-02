using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Crossroads.Domain.Entities.Interfaces;

namespace Crossroads.Domain.DataAccess.Interfaces
{
    public interface IRepository
    {
        int SaveChanges();
    }
}
