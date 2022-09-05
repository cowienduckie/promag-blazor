using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProMag.Server.Core.Domain.Entities;

namespace ProMag.Server.Infrastructure.Configurations;

public class ProjectManagerConfiguration : BaseConfiguration<ProjectManager>
{
    public override void ConfigureEntity(EntityTypeBuilder<ProjectManager> entity)
    {
        entity.HasKey(e => e.Id);

        entity.HasOne(e => e.UserAccount)
            .WithMany(u => u.ProjectManagers)
            .HasForeignKey(e => e.UserId);

        entity.HasOne(e => e.Project)
            .WithMany(p => p.ProjectManagers)
            .HasForeignKey(e => e.ProjectId);
    }
}