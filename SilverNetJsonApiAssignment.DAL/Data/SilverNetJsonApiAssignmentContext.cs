using Microsoft.EntityFrameworkCore;
using SilverNetJsonApiAssignment.Configurations;
using SilverNetJsonApiAssignment.Entities;

namespace SilverNetJsonApiAssignment.Data
{
    public class SilverNetJsonApiAssignmentContext : DbContext
    {
        public SilverNetJsonApiAssignmentContext(DbContextOptions<SilverNetJsonApiAssignmentContext> options)
     : base(options)
        {
        }
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
