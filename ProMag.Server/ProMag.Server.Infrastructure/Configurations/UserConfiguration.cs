using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProMag.Server.Core.Domain.Entities;

namespace ProMag.Server.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.HasKey(e => new {e.Id});

        entity.HasMany<Employee>(e => e.Employees)
            .WithOne(e => e.UserAccount)
            .HasForeignKey(e => e.UserId);
    }
}