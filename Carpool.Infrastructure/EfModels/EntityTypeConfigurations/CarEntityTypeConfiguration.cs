using Carpool.Core.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carpool.Infrastructure.EfModels.EntityTypeConfigurations
{
    public class CarEntityTypeConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder
                .Property(car => car.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property(car => car.Type)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property(car => car.Color)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(car => car.LicensePlate)
                .HasMaxLength(10)
                .IsRequired();

            builder
                .HasIndex(car => car.LicensePlate)
                .IsUnique();

            builder
                .Property(car => car.Capacity);
        }
    }
}
