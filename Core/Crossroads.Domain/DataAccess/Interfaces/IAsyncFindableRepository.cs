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
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true);
        Task<TEntity?> GetByIdAsync(Guid id, bool tracking = true);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null);
    }
}
