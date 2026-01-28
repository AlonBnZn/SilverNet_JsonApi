using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NServiceBus.Service.Entities;

namespace NServiceBus.Service.Configurations
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedNever();
            builder.HasMany(t => t.Users).WithOne(u => u.Tenant).IsRequired(true);
        }
    }
}
