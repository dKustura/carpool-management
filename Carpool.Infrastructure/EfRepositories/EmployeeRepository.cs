using Carpool.Core.Model.Entities;
using Carpool.Core.Repositories;
using Carpool.Infrastructure.EfModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Carpool.Infrastructure.EfRepositories
{
    public class EmployeeRepository : Repository<Employee, long>, IEmployeeRepository
    {
        public EmployeeRepository(CarpoolContext context) : base(context) { }

        public override async Task<Employee> GetById(long id)
        {
            return await GetTableQueryable().FirstOrDefaultAsync(employee => employee.EmployeeId == id);
        }
    }
}
