using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NServiceBus.Service.Entities;

namespace NServiceBus.Service.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedNever();
            builder.HasOne(u => u.Tenant).WithMany(t => t.Users).IsRequired(true);
        }
    }
}
