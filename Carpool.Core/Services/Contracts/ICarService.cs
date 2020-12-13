using Carpool.Core.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carpool.Core.Services.Contracts
{
    public interface ICarService
    {
        Task<IEnumerable<CarResult>> GetAll();
    }
}
