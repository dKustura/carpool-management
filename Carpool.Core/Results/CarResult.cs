using Carpool.Core.Model.Entities;

namespace Carpool.Core.Results
{
    public class CarResult
    {
        public long CarId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public int Capacity { get; set; }

        public CarResult() { }

        public CarResult(Car model)
        {
            CarId = model.CarId;
            Name = model.Name;
            Type = model.Type;
            Color = model.Color;
            LicensePlate = model.LicensePlate;
            Capacity = model.Capacity;
        }
    }
}
