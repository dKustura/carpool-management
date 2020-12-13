using Carpool.Core.Model.Entities;
using Carpool.Infrastructure.EfModels.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Carpool.Infrastructure.EfModels
{
    public class CarpoolContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<TravelPlan> TravelPlans { get; set; }
        public DbSet<EmployeeTravelPlanMapping> EmployeeTravelPlanMappings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=carpool.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new CarEntityTypeConfiguration().Configure(modelBuilder.Entity<Car>());
            new EmployeeEntityTypeConfiguration().Configure(modelBuilder.Entity<Employee>());
            new TravelPlanEntityTypeConfiguration().Configure(modelBuilder.Entity<TravelPlan>());
            new EmployeeTravelPlanMappingEntityTypeConfiguration().Configure(modelBuilder.Entity<EmployeeTravelPlanMapping>());
        }
    }
}
