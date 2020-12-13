using Carpool.Core.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carpool.Core.Repositories
{
    public interface ITravelPlanRepository : IRepository<TravelPlan, long>
    {
        Task<IEnumerable<TravelPlan>> GetByCarId(long carId, long? excludedId);
        Task<IEnumerable<TravelPlan>> GetByEmployeeId(long employeeId, long? excludedId);
    }
}
