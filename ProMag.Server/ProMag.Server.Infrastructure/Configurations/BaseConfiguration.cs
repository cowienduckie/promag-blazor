using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProMag.Server.Core.Domain.Entities;

namespace ProMag.Server.Infrastructure.Configurations;

public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    public string SchemaName => "dbo";

    public string TableName => typeof(TEntity).Name;

    public virtual void Configure(EntityTypeBuilder<TEntity> entity)
    {
        entity.ToTable(TableName, SchemaName);

        ConfigureEntity(entity);

        entity.Property(e => e.CreateTime)
            .HasColumnOrder(997);

        entity.Property(e => e.LastModified)
            .HasColumnOrder(998);

        entity.Property(e => e.IsDelete)
            .HasColumnOrder(999);
    }

    public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> entity);
}