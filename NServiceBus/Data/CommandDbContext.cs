using Microsoft.EntityFrameworkCore;
using NServiceBus.Configurations;
using NServiceBus.Entities;

namespace NServiceBus.Data
{
    public class CommandDbContext : DbContext
    {
        public CommandDbContext(DbContextOptions<CommandDbContext> options) : base(options) { }

        public DbSet<Tenant> Tenants { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TenantConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
