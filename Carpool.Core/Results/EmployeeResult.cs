using Carpool.Core.Model.Entities;

namespace Carpool.Core.Results
{
    public class EmployeeResult
    {
        public long EmployeeId { get; set; }
        public string Name { get; set; }
        public bool IsDriver { get; set; }

        public EmployeeResult(Employee model)
        {
            EmployeeId = model.EmployeeId;
            Name = model.Name;
            IsDriver = model.IsDriver;
        }
    }
}
