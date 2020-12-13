using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carpool.Core.Repositories;
using Carpool.Infrastructure.EfModels;
using Microsoft.EntityFrameworkCore;

namespace Carpool.Infrastructure.EfRepositories
{
    public abstract class Repository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : class
    {
        protected CarpoolContext Context { get; }

        public Repository(CarpoolContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected DbSet<TEntity> GetTable()
        {
            return Context.Set<TEntity>();
        }
        protected virtual IQueryable<TEntity> GetTableQueryable()
        {
            return GetTable();
        }

        public async Task Add(TEntity entity)
        {
            await GetTable().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            GetTable().Remove(entity);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await GetTableQueryable().AsNoTracking().ToListAsync();
        }

        public abstract Task<TEntity> GetById(TPrimaryKey id);

        public void Update(TEntity entity)
        {
            GetTable().Update(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            GetTable().RemoveRange(entities);
        }
    }
}
