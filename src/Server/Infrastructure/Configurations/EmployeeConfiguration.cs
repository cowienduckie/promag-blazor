using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProMag.Server.Core.Domain.Entities;

namespace ProMag.Server.Infrastructure.Configurations;

public class EmployeeConfiguration : BaseConfiguration<Employee>
{
    public override void ConfigureEntity(EntityTypeBuilder<Employee> entity)
    {
        entity.HasKey(e => e.Id);

        entity.HasOne(e => e.UserAccount)
            .WithMany(u => u.Employees)
            .HasForeignKey(e => e.UserId);

        entity.HasMany(e => e.Members)
            .WithOne(tm => tm.Employee)
            .HasForeignKey(tm => tm.EmployeeId);

        entity.HasMany(e => e.Assignments)
            .WithOne(a => a.Employee)
            .HasForeignKey(a => a.EmployeeId);
    }
}