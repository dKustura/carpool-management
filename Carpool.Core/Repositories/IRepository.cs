using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carpool.Core.Repositories
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(TPrimaryKey id);
        Task Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
    }
}
