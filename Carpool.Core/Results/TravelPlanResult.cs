using Carpool.Core.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpool.Core.Results
{
    public class TravelPlanResult
    {
        public long TravelPlanId { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CarResult Car { get; set; }
        public IEnumerable<EmployeeResult> Employees { get; set; }

        public TravelPlanResult(TravelPlan model)
        {
            TravelPlanId = model.TravelPlanId;
            StartLocation = model.StartLocation;
            EndLocation = model.EndLocation;
            StartDate = model.StartDate;
            EndDate = model.EndDate;
            Car = new CarResult(model.Car);
            Employees = model.EmployeeTravelPlanMappings.Select(mapping => new EmployeeResult(mapping.Employee));
        }
    }
}
