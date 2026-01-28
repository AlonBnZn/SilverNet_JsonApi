using Microsoft.EntityFrameworkCore;
using NServiceBus.Service.Configurations;
using NServiceBus.Service.Entities;

namespace NServiceBus.Service.Data
{
    public class CommandDbContext : DbContext
    {
        public CommandDbContext(DbContextOptions<CommandDbContext> options) : base(options) { }

        public DbSet<Tenant> Tenants { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Subscription> Subscriptions { get; set; } = null!;

        public DbSet<Billing> Billings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TenantConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new BillingConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());
        }
    }
}
