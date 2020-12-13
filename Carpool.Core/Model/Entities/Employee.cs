
using System.Collections.Generic;

namespace Carpool.Core.Model.Entities
{
    public class Employee
    {
        public long EmployeeId { get; set; }
        public string Name { get; set; }
        public bool IsDriver { get; set; }

        public ICollection<EmployeeTravelPlanMapping> EmployeeTravelPlanMappings { get; set; }
    }
}
