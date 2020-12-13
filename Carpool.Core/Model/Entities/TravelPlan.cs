using System;
using System.Collections.Generic;

namespace Carpool.Core.Model.Entities
{
    public class TravelPlan
    {
        public long TravelPlanId { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long CarId { get; set; }

        public Car Car { get; set; }
        public ICollection<EmployeeTravelPlanMapping> EmployeeTravelPlanMappings { get; set; }
    }
}
