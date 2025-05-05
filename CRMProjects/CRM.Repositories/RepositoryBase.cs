using CRM.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected AppDbContext Context { get; }

        protected DbSet<TEntity> DbSet { get; }

        public RepositoryBase(AppDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }
        
        public TEntity Add(TEntity entity)
        {
            return DbSet.Add(entity).Entity;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public async Task<TEntity> FindAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<List<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }

        public async Task<List<TEntity>> GetAllByWhereAsync(Expression<Func<TEntity, bool>> match, bool disableTracking = false)
        {
            return disableTracking
              ? await DbSet.AsNoTracking().Where(match).ToListAsync()
              : await DbSet.Where(match).ToListAsync();
        }

        public async Task<TEntity> GetFirstWhereAsync(Expression<Func<TEntity, bool>> match, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            return (include != null)
              ? await include(DbSet).AsNoTracking().FirstOrDefaultAsync(match)
              : await DbSet.AsNoTracking().FirstOrDefaultAsync(match);
        }

        public async Task<TEntity> ReloadAsync(TEntity entity)
        {
            await Context.Entry(entity).ReloadAsync();
            return entity;
        }

        public TEntity Remove(TEntity entity)
        {
            return DbSet.Remove(entity).Entity;
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public TEntity Update(TEntity entity)
        {
            var id = GetKey(entity);
            var data = DbSet.Find(id);
            var state = Context.Entry<TEntity>(data).State;
            Context.Entry(data).State = EntityState.Detached;
            return DbSet.Update(entity).Entity;
        }
        
        public void Detached(TEntity entity)
        {
            var id = GetKey(entity);
            var data = DbSet.Find(id);
            var state = Context.Entry<TEntity>(data).State;
            Context.Entry(data).State = EntityState.Detached;
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
        }

        public async Task DbSaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (include != null)
            {
                query = include(query);
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return await query.ToListAsync();
        }

        public virtual int GetKey(TEntity entity)
        {
            var keyName = Context.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties
                .Select(x => x.Name).Single();

            return (int)entity.GetType().GetProperty(keyName).GetValue(entity, null);
        }

    }
}
