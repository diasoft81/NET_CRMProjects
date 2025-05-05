using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CRM.Repositories.Interfaces
{
    public interface IRepositoryBase<TEntity>
    {
        Task<List<TEntity>> GetAllByWhereAsync(Expression<Func<TEntity, bool>> match,
         bool disableTracking = false);

        Task<List<TEntity>> GetAllAsync(Func<IQueryable<TEntity>,
          IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true);

        Task<List<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>,
            IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true);

        Task<TEntity> GetFirstWhereAsync(Expression<Func<TEntity, bool>> match,
          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        Task<TEntity> FindAsync(object id);

        TEntity Update(TEntity entity);
        void Detached(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);

        TEntity Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        TEntity Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        Task<TEntity> ReloadAsync(TEntity entity);
        Task DbSaveAsync();
    }
}
