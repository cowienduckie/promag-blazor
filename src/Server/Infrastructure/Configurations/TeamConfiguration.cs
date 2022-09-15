using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProMag.Server.Core.Domain.Entities;

namespace ProMag.Server.Infrastructure.Configurations;

public class TeamConfiguration : BaseConfiguration<Team>
{
    public override void ConfigureEntity(EntityTypeBuilder<Team> entity)
    {
        entity.HasKey(e => e.Id);

        entity.HasMany(e => e.Members)
            .WithOne(tm => tm.Team)
            .HasForeignKey(tm => tm.TeamId);
    }
}