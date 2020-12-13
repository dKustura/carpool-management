using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpool.Core.Requests
{
    public class TravelPlanRequest
    {
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long CarId { get; set; }
        public IEnumerable<long> EmployeeIds { get; set; }
    }

    public class TravelPlanCreateRequest : TravelPlanRequest { }

    public class TravelPlanUpdateRequest : TravelPlanRequest {
        public long TravelPlanId { get; set; }
    }
}
