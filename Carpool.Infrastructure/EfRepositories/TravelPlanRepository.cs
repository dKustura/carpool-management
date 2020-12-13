using Carpool.Core.Model.Entities;
using Carpool.Core.Repositories;
using Carpool.Infrastructure.EfModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Infrastructure.EfRepositories
{
    public class TravelPlanRepository : Repository<TravelPlan, long>, ITravelPlanRepository
    {
        public TravelPlanRepository(CarpoolContext context) : base(context) { }

        public override async Task<IEnumerable<TravelPlan>> GetAll()
        {
            return await GetTableQueryable()
                .Include(travelPlan => travelPlan.Car)
                .Include(travelPlan => travelPlan.EmployeeTravelPlanMappings)
                    .ThenInclude(mapping => mapping.Employee)
                .ToListAsync();
        }

        public override async Task<TravelPlan> GetById(long id)
        {
            return await GetTableQueryable()
                .Include(travelPlan => travelPlan.Car)
                .Include(travelPlan => travelPlan.EmployeeTravelPlanMappings)
                    .ThenInclude(mapping => mapping.Employee)
                .FirstOrDefaultAsync(travelPlan => travelPlan.TravelPlanId == id);
        }

        public async Task<IEnumerable<TravelPlan>> GetByCarId(long carId, long? excludeId)
        {
            return await GetTableQueryable()
                .Where(travelPlan => travelPlan.CarId == carId && travelPlan.TravelPlanId != excludeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TravelPlan>> GetByEmployeeId(long employeeId, long? excludeId)
        {
            return await GetTableQueryable()
                .Include(travelPlan => travelPlan.EmployeeTravelPlanMappings)
                .Where(travelPlan => travelPlan.EmployeeTravelPlanMappings.Any(mapping => mapping.EmployeeId == employeeId 
                    && travelPlan.TravelPlanId != excludeId))
                .ToListAsync();
        }
    }
}
