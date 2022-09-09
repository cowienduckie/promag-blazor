using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProMag.Server.Core.Domain.Entities;

namespace ProMag.Server.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.HasKey(e => new { e.Id });

        entity.HasMany(e => e.Employees)
            .WithOne(e => e.UserAccount)
            .HasForeignKey(e => e.UserId);

        entity.HasMany(e => e.ProjectManagers)
            .WithOne(pm => pm.UserAccount)
            .HasForeignKey(pm => pm.UserId);
    }
}