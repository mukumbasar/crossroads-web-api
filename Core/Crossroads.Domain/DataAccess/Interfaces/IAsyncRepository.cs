using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Domain.DataAccess.Interfaces
{
    public interface IAsyncRepository
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
