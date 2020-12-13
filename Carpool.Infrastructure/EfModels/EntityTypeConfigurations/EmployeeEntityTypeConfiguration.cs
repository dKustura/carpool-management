using Carpool.Core.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carpool.Infrastructure.EfModels.EntityTypeConfigurations
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder
                .Property(employee => employee.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property(employee => employee.IsDriver)
                .HasDefaultValue(false);
        }
    }
}
