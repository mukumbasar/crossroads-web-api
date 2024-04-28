using Crossroads.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Domain.DataAccess.Interfaces
{
    public interface IAsyncFindableRepository<TEntity> : IAsyncQueryableRepository<TEntity>, IAsyncRepository where TEntity : BaseEntity
    {
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null);
        Task<TEntity> GetAsync(bool asNoTracking = true, Expression<Func<TEntity, bool>>? filter = null, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity?> GetByIdAsync(Guid id, bool tracking = true);
    }
}
