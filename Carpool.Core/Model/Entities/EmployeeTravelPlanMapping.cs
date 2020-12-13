
namespace Carpool.Core.Model.Entities
{
    public class EmployeeTravelPlanMapping
    {
        public long EmployeeTravelPlanMappingId { get; set; }
        public long EmployeeId { get; set; }
        public long TravelPlanId { get; set; }

        public Employee Employee { get; set; }
        public TravelPlan TravelPlan { get; set; }
    }
}
