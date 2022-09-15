using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProMag.Server.Core.Domain.Entities;

namespace ProMag.Server.Infrastructure.Configurations;

public class SubTaskConfiguration : BaseConfiguration<SubTask>
{
    public override void ConfigureEntity(EntityTypeBuilder<SubTask> entity)
    {
        entity.HasKey(e => e.Id);

        entity.HasMany(e => e.Assignments)
            .WithOne(a => a.SubTask)
            .HasForeignKey(a => a.SubTaskId);

        entity.HasOne(e => e.MainTask)
            .WithMany(mt => mt.SubTasks)
            .HasForeignKey(e => e.MainTaskId);
    }
}