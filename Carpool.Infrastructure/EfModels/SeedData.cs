using Carpool.Core.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Carpool.Infrastructure.EfModels
{
    public class SeedData
    {
        public static void SaveInitialData()
        {
            using var dbContext = new CarpoolContext();

            dbContext.Database.EnsureCreated();
            if (!dbContext.Cars.Any())
            {
                var cars = GetCarSeedData();
                dbContext.Cars.AddRange(cars);
            }
            if (!dbContext.Employees.Any())
            {
                var employees = GetEmployeeSeedData();
                dbContext.Employees.AddRange(employees);
            }
            dbContext.SaveChanges();
        }

        private static IEnumerable<Car> GetCarSeedData()
        {
            return new List<Car>
            {
                new Car { Name = "Blue VW Tiguan for travel and city rides", Type = "VW Tiguan", Color = "Blue", LicensePlate = "ZG 5689-GF", Capacity = 5},
                new Car { Name = "Red Nissan Qashqai for travel", Type = "Nissan Qashqai", Color = "Red", LicensePlate = "ZG 9674-HB", Capacity = 5},
                new Car { Name = "Gray Audi A5 for travel", Type = "Audi A5", Color = "Gray", LicensePlate = "PU 872-PA", Capacity = 5},
                new Car { Name = "Black Mercedes-Benz C-Class for travel", Type = "Mercedes-Benz C-Class", Color = "Black", LicensePlate = "PU 165-TR", Capacity = 5},
                new Car { Name = "Yellow VW Golf for travel and city rides", Type = "VW Golf", Color = "Yellow", LicensePlate = "DA 715-DF", Capacity = 4},
                new Car { Name = "Blue Ford Fiesta for quick support", Type = "Ford Fiesta", Color = "Blue", LicensePlate = "DA 889-LT", Capacity = 4},
                new Car { Name = "White Honda Civic city rides", Type = "Honda Civic", Color = "White", LicensePlate = "ZD 258-DR", Capacity = 5},
                new Car { Name = "Gray Skoda Octavia for travel", Type = "Skoda Octavia", Color = "Gray", LicensePlate = "ZD 742-DF", Capacity = 5},
                new Car { Name = "Red Citroen C3 for travel and quick support", Type = "Citroen C3", Color = "Red", LicensePlate = "ST 325-BA", Capacity = 5},
                new Car { Name = "Orange Renault Captur for travel", Type = "Renault Captur", Color = "Orange", LicensePlate = "ST 837-OT", Capacity = 4 },
                new Car { Name = "Black BMW X3 for travel", Type = "BMW X3", Color = "Black", LicensePlate = "OS 255-BG", Capacity = 5},
                new Car { Name = "Yellow Peugeot 208 for travel and city rides", Type = "Peugeot 208", Color = "Yellow", LicensePlate = "OS 565-KA", Capacity = 5}
            };
        }

        private static IEnumerable<Employee> GetEmployeeSeedData()
        {
            return new List<Employee>
            {
                new Employee { Name = "Ivo Bogdanovic", IsDriver = true },
                new Employee { Name = "Ivona Kolaric", IsDriver = true },
                new Employee { Name = "Lovro Kosar", IsDriver = true },
                new Employee { Name = "Helena Jankovic", IsDriver = true },
                new Employee { Name = "Iva Milic", IsDriver = false },
                new Employee { Name = "Bernard Golub", IsDriver = false },
                new Employee { Name = "Vedran Stankic", IsDriver = false },
                new Employee { Name = "Jadranka Novak", IsDriver = false },
                new Employee { Name = "Franka Tomic", IsDriver = true },
                new Employee { Name = "Ivan Modric", IsDriver = true },
                new Employee { Name = "Ivan Zoric", IsDriver = true },
                new Employee { Name = "Dino Vukovic", IsDriver = true },
                new Employee { Name = "Lovre Zupan", IsDriver = true },
                new Employee { Name = "Domagoj Kalmeta", IsDriver = false },
                new Employee { Name = "Stanislav Loncar", IsDriver = false },
                new Employee { Name = "Tomislav Pavlovic", IsDriver = false },
                new Employee { Name = "Robert Kovacic", IsDriver = false },
                new Employee { Name = "Stipe Vukic", IsDriver = true },
                new Employee { Name = "Lara Vrdoljak", IsDriver = true },
                new Employee { Name = "Mihovil Maric", IsDriver = false },
                new Employee { Name = "Franko Horvatincic", IsDriver = true },
                new Employee { Name = "Ana Vrsaljko", IsDriver = true },
                new Employee { Name = "Sime Malenica", IsDriver = false },
                new Employee { Name = "Ena Kovac", IsDriver = true },
                new Employee { Name = "Josipa Horvat", IsDriver = false }
            };
        }
    }
}
