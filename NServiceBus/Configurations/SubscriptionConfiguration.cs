using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NServiceBus.Service.Entities;

namespace NServiceBus.Service.Configurations
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.Amount).IsRequired().HasPrecision(18, 2);
            builder.Property(s => s.CreationDate).IsRequired();
            builder.HasOne(s => s.Tenant);
        }
    }
}
