using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SilveNetJsonApiAssignment.Service.Resources;

namespace SilveNetJsonApiAssignment.Service.Configurations
{
    public class UserResourceConfiguration : IEntityTypeConfiguration<UserResource>
    {
        public void Configure(EntityTypeBuilder<UserResource> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.FirstName).IsRequired(true).HasMaxLength(10);
            builder.Property(u => u.LastName).IsRequired(true).HasMaxLength(10);
            builder.Property(u => u.Phone).IsRequired(true).HasMaxLength(12);
            builder.Property(u => u.Email).IsRequired(true).HasMaxLength(50);
            builder.Property(u => u.IdNumber).IsRequired(false).HasMaxLength(9);
            builder.Property(u => u.CreationDate).IsRequired(true);
            builder.HasOne(u => u.Tenant).WithMany(t => t.Users).IsRequired(true);
        }
    }
}
