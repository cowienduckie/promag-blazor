using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProMag.Server.Core.Domain.Entities;

namespace ProMag.Server.Infrastructure.Configurations;

public class ClientConfiguration : BaseConfiguration<Client>
{
    public override void ConfigureEntity(EntityTypeBuilder<Client> entity)
    {
        entity.HasKey(e => e.Id);

        entity.HasMany(e => e.Contacts)
            .WithOne(c => c.Client)
            .HasForeignKey(c => c.ClientId);
    }
}

public class ClientContactConfiguration : BaseConfiguration<ClientContact>
{
    public override void ConfigureEntity(EntityTypeBuilder<ClientContact> entity)
    {
        entity.HasKey(e => e.Id);

        entity.HasOne(e => e.Client)
            .WithMany(c => c.Contacts)
            .HasForeignKey(e => e.ClientId);
    }
}