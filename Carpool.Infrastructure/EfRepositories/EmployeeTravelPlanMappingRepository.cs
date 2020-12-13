using Carpool.Core.Model.Entities;
using Carpool.Core.Repositories;
using Carpool.Infrastructure.EfModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Carpool.Infrastructure.EfRepositories
{
    public class EmployeeTravelPlanMappingRepository :
        Repository<EmployeeTravelPlanMapping, long>, IEmployeeTravelPlanMappingRepository
    {
        public EmployeeTravelPlanMappingRepository(CarpoolContext context) : base(context) { }

        public override async Task<EmployeeTravelPlanMapping> GetById(long id)
        {
            return await GetTableQueryable()
                .FirstOrDefaultAsync(employee => employee.EmployeeTravelPlanMappingId == id);
        }
    }
}
