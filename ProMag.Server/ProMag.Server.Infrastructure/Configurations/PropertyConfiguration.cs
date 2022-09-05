using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProMag.Server.Core.Domain.Entities;

namespace ProMag.Server.Infrastructure.Configurations;

public class PropertyConfiguration : BaseConfiguration<Property>
{
    public override void ConfigureEntity(EntityTypeBuilder<Property> entity)
    {
        entity.HasKey(e => e.Id);

        entity.HasOne(e => e.Project)
            .WithMany(p => p.DefaultProperties)
            .HasForeignKey(e => e.ProjectId);

        entity.HasOne(e => e.Type)
            .WithMany(t => t.Properties)
            .HasForeignKey(e => e.TypeId);

        entity.HasOne(e => e.MainTask)
            .WithMany(mt => mt.Properties)
            .HasForeignKey(e => e.MainTaskId);

        entity.HasMany(e => e.SubProperties)
            .WithOne(sp => sp.Property)
            .HasForeignKey(sp => sp.PropertyId);
    }
}

public class PropertyTypeConfiguration : BaseConfiguration<PropertyType>
{
    public override void ConfigureEntity(EntityTypeBuilder<PropertyType> entity)
    {
        entity.HasKey(e => e.Id);

        entity.HasMany(e => e.Properties)
            .WithOne(p => p.Type)
            .HasForeignKey(p => p.TypeId);
    }
}

public class SubPropertyConfiguration : BaseConfiguration<SubProperty>
{
    public override void ConfigureEntity(EntityTypeBuilder<SubProperty> entity)
    {
        entity.HasKey(e => e.Id);

        entity.HasOne(e => e.Property)
            .WithMany(p => p.SubProperties)
            .HasForeignKey(e => e.PropertyId);
    }
}
