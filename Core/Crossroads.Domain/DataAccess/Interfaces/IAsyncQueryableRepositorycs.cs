using Crossroads.Domain.Entities.Bases;
using Crossroads.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Domain.DataAccess.Interfaces
{
    public interface IAsyncQueryableRepository<TEntity> : IAsyncRepository where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = true, Expression<Func<TEntity, bool>>? filter = null, params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
