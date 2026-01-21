using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SilveNetJsonApiAssignment.Service.Resources;

namespace SilveNetJsonApiAssignment.Service.Configurations
{
    public class TenantResourceConfiguration : IEntityTypeConfiguration<TenantResource>
    {
        public void Configure(EntityTypeBuilder<TenantResource> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.Name).IsRequired(true).HasMaxLength(20);
            builder.Property(t => t.Phone).IsRequired(true).HasMaxLength(12);
            builder.Property(t => t.Email).IsRequired(true).HasMaxLength(50);
            builder.Property(t => t.CreationDate).IsRequired(true);
            builder.HasMany(t => t.Users).WithOne(u => u.Tenant).IsRequired(true);

        }
    }
}
