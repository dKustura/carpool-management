using System.Threading.Tasks;
using Carpool.Core.Model.Entities;
using Carpool.Core.Repositories;
using Carpool.Infrastructure.EfModels;
using Microsoft.EntityFrameworkCore;

namespace Carpool.Infrastructure.EfRepositories
{
    public class CarRepository : Repository<Car, long>, ICarRepository
    {
        public CarRepository(CarpoolContext context) : base(context) { }

        public override async Task<Car> GetById(long id)
        {
            return await GetTableQueryable().FirstOrDefaultAsync(car => car.CarId == id);
        }
    }
}
