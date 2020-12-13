using Carpool.Core.Results;
using Carpool.Core.Services.Contracts;
using Carpool.Core.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Core.Services
{
    public class CarService : Service, ICarService
    {
        public CarService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<IEnumerable<CarResult>> GetAll()
        {
            var models = await unitOfWork.Cars.GetAll();

            var results = models.Select(model => new CarResult(model));
            return results;
        }
    }
}
