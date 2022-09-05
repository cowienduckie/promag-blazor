using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProMag.Server.Core.Domain.Entities;
using TaskStatus = ProMag.Server.Core.Domain.Entities.TaskStatus;

namespace ProMag.Server.Infrastructure.Configurations;

public class MainTaskConfiguration : BaseConfiguration<MainTask>
{
    public override void ConfigureEntity(EntityTypeBuilder<MainTask> entity)
    {
        entity.HasKey(e => e.Id);

        entity.HasOne(e => e.Project)
            .WithMany(p => p.MainTasks)
            .HasForeignKey(e => e.ProjectId);

        entity.HasOne(e => e.Milestone)
            .WithMany(m => m.MainTasks)
            .HasForeignKey(e => e.MilestoneId);

        entity.HasOne(e => e.Status)
            .WithMany(s => s.MainTasks)
            .HasForeignKey(e => e.StatusId);

        entity.HasMany(e => e.SubTasks)
            .WithOne(st => st.MainTask)
            .HasForeignKey(st => st.MainTaskId);

        entity.HasMany(e => e.Properties)
            .WithOne(p => p.MainTask)
            .HasForeignKey(p => p.MainTaskId);
    }
}

public class TaskStatusConfiguration : BaseConfiguration<TaskStatus>
{
    public override void ConfigureEntity(EntityTypeBuilder<TaskStatus> entity)
    {
        entity.HasKey(e => e.Id);

        entity.HasMany(e => e.MainTasks)
            .WithOne(mt => mt.Status)
            .HasForeignKey(mt => mt.StatusId);
    }
}