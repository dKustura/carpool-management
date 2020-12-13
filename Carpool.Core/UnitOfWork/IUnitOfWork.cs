using Carpool.Core.Repositories;
using System.Threading.Tasks;

namespace Carpool.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task Commit();

        IRepository<TEntity, TPrimaryKey> GetRepository<TEntity, TPrimaryKey>() where TEntity : class;

        ICarRepository Cars { get; }
        IEmployeeRepository Employees { get; }
        ITravelPlanRepository TravelPlans { get; }
        IEmployeeTravelPlanMappingRepository EmployeeTravelPlanMappings { get; }
    }
}
