using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProMag.Server.Core.Domain.Entities;

namespace ProMag.Server.Infrastructure.Configurations;

public class TeamMemberConfiguration : BaseConfiguration<TeamMember>
{
    public override void ConfigureEntity(EntityTypeBuilder<TeamMember> entity)
    {
        entity.HasKey(e => new {e.Id});

        entity.HasOne(e => e.Employee)
            .WithMany(e => e.Members)
            .HasForeignKey(e => e.EmployeeId);

        entity.HasOne(e => e.Team)
            .WithMany(t => t.Members)
            .HasForeignKey(e => e.TeamId);

        entity.HasOne(e => e.TeamRole)
            .WithMany(tr => tr.Members)
            .HasForeignKey(e => e.TeamRoleId);
    }
}