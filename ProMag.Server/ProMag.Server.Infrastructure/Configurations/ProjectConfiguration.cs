using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProMag.Server.Core.Domain.Entities;

namespace ProMag.Server.Infrastructure.Configurations;

public class ProjectConfiguration : BaseConfiguration<Project>
{
    public override void ConfigureEntity(EntityTypeBuilder<Project> entity)
    {
        entity.HasKey(e => e.Id);

        entity.HasOne(e => e.Status)
            .WithMany(s => s.Projects)
            .HasForeignKey(e => e.StatusId);

        entity.HasMany(e => e.Clients)
            .WithMany(c => c.Projects)
            .UsingEntity<ProjectClient>(
                e => e
                    .HasOne(pc => pc.Client)
                    .WithMany(c => c.ProjectClients)
                    .HasForeignKey(pc => pc.ClientId),
                e => e
                    .HasOne(pc => pc.Project)
                    .WithMany(p => p.ProjectClients)
                    .HasForeignKey(pc => pc.ProjectId),
                e => { e.HasKey(pc => new {pc.ProjectId, pc.ClientId}); });

        entity.HasMany(e => e.ProjectManagers)
            .WithOne(pm => pm.Project)
            .HasForeignKey(pm => pm.ProjectId);

        entity.HasMany(e => e.Milestones)
            .WithOne(m => m.Project)
            .HasForeignKey(m => m.ProjectId);

        entity.HasMany(e => e.MainTasks)
            .WithOne(mt => mt.Project)
            .HasForeignKey(mt => mt.ProjectId);

        entity.HasMany(e => e.DefaultProperties)
            .WithOne(p => p.Project)
            .HasForeignKey(p => p.ProjectId);
    }
}

public class ProjectStatusConfiguration : BaseConfiguration<ProjectStatus>
{
    public override void ConfigureEntity(EntityTypeBuilder<ProjectStatus> entity)
    {
        entity.HasKey(e => e.Id);

        entity.HasMany(e => e.Projects)
            .WithOne(p => p.Status)
            .HasForeignKey(p => p.StatusId);
    }
}