using Carpool.Core.Results;
using Carpool.Core.Services.Contracts;
using Carpool.Core.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Core.Services
{
    public class EmployeeService : Service, IEmployeeService
    {
        public EmployeeService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<IEnumerable<EmployeeResult>> GetAll()
        {
            var models = await unitOfWork.Employees.GetAll();

            var results = models.Select(model => new EmployeeResult(model));
            return results;
        }
    }
}
