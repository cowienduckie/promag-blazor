using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Infrastructure.Extensions;

namespace ProMag.Server.Infrastructure.Configurations;

public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    public string SchemaName => "dbo";

    public string TableName => typeof(TEntity).Name;

    public virtual void Configure(EntityTypeBuilder<TEntity> entity)
    {
        entity.ToTable(TableName, SchemaName);

        ConfigureEntity(entity);
    }

    public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> entity);
}