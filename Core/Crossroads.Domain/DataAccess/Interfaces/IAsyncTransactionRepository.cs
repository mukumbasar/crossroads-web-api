using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Crossroads.Domain.DataAccess.Interfaces
{
    public interface IAsyncTransactionRepository
    {
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task<IExecutionStrategy> CreateExecutionStrategy();
    }
}
