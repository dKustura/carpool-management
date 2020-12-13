using Carpool.Core.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carpool.Infrastructure.EfModels.EntityTypeConfigurations
{
    public class TravelPlanEntityTypeConfiguration : IEntityTypeConfiguration<TravelPlan>
    {
        public void Configure(EntityTypeBuilder<TravelPlan> builder)
        {
            builder
                .Property(travelPlan => travelPlan.StartLocation)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(travelPlan => travelPlan.EndLocation)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(travelPlan => travelPlan.StartDate)
                .IsRequired();

            builder
                .Property(travelPlan => travelPlan.EndDate)
                .IsRequired();

            builder
                .HasOne(travelPlan => travelPlan.Car)
                .WithMany(car => car.TravelPlans)
                .HasForeignKey(travelPlan => travelPlan.CarId);
        }
    }
}
