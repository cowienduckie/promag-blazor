using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProMag.Server.Core.Domain.Entities;

namespace ProMag.Server.Infrastructure.Configurations;

public class MilestoneConfiguration : BaseConfiguration<Milestone>
{
    public override void ConfigureEntity(EntityTypeBuilder<Milestone> entity)
    {
        entity.HasKey(e => e.Id);

        entity.HasOne(e => e.Status)
            .WithMany(s => s.Milestones)
            .HasForeignKey(e => e.StatusId);

        entity.HasOne(e => e.Project)
            .WithMany(p => p.Milestones)
            .HasForeignKey(e => e.ProjectId);

        entity.HasMany(e => e.MainTasks)
            .WithOne(mt => mt.Milestone)
            .HasForeignKey(mt => mt.MilestoneId);
    }
}

public class MilestoneStatusConfiguration : BaseConfiguration<MilestoneStatus>
{
    public override void ConfigureEntity(EntityTypeBuilder<MilestoneStatus> entity)
    {
        entity.HasKey(e => e.Id);

        entity.HasMany(e => e.Milestones)
            .WithOne(m => m.Status)
            .HasForeignKey(m => m.StatusId);
    }
}
