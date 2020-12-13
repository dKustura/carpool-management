using Carpool.Core.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carpool.Infrastructure.EfModels.EntityTypeConfigurations
{
    public class EmployeeTravelPlanMappingEntityTypeConfiguration : IEntityTypeConfiguration<EmployeeTravelPlanMapping>
    {
        public void Configure(EntityTypeBuilder<EmployeeTravelPlanMapping> builder)
        {
            builder
                .HasOne(mapping => mapping.Employee)
                .WithMany(car => car.EmployeeTravelPlanMappings)
                .HasForeignKey(travelPlan => travelPlan.EmployeeId);

            builder
                .HasOne(mapping => mapping.TravelPlan)
                .WithMany(car => car.EmployeeTravelPlanMappings)
                .HasForeignKey(travelPlan => travelPlan.TravelPlanId);
        }
    }
}
