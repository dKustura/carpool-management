using Carpool.Core.Requests;
using Carpool.Core.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carpool.Core.Services.Contracts
{
    public interface ITravelPlanService
    {
        Task<IEnumerable<TravelPlanResult>> GetAll();
        Task<TravelPlanResult> GetById(long id);
        Task<TravelPlanResult> Create(TravelPlanCreateRequest request);
        Task Update(TravelPlanUpdateRequest request);
        Task Delete(long id);
    }
}
