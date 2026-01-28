using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NserviceBus.Messages.Subscriptions;
using NServiceBus.Service.Entities;

namespace NServiceBus.Service.Configurations
{
    public class BillingConfiguration : IEntityTypeConfiguration<Billing>
    {
        public void Configure(EntityTypeBuilder<Billing> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.Amount).IsRequired().HasPrecision(18, 2);
            builder.HasOne(s => s.Subscription);

            var billingCycleBuilder = builder.OwnsOne<BillingCycle>(x => x.BillingCycle);

            billingCycleBuilder.Property(x => x.Year).HasColumnName("BillingCycle_Year").IsRequired();
            billingCycleBuilder.Property(x => x.Month).HasColumnName("BillingCycle_Month").IsRequired();
        }
    }
}
