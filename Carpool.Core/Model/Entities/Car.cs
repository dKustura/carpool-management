using System.Collections.Generic;

namespace Carpool.Core.Model.Entities
{
    public class Car
    {
        public long CarId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public int Capacity { get; set; }

        public ICollection<TravelPlan> TravelPlans { get; set; }
    }
}
