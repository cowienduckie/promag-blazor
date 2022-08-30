using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProMag.Server.Core.Domain.Entities;

namespace ProMag.Server.Infrastructure.Configurations;

public class TeamRoleConfiguration : BaseConfiguration<TeamRole>
{
    public override void ConfigureEntity(EntityTypeBuilder<TeamRole> entity)
    {
        entity.HasKey(e => e.Id);

        entity.HasMany(e => e.Members)
            .WithOne(tm => tm.TeamRole)
            .HasForeignKey(tm => tm.TeamRoleId);
    }
}