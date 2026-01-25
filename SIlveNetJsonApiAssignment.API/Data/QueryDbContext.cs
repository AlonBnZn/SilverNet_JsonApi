using Microsoft.EntityFrameworkCore;
using SilveNetJsonApiAssignment.Service.Configurations;
using SilveNetJsonApiAssignment.Service.Resources;
using SilverNetJsonApiAssignment.Configurations;

namespace SilveNetJsonApiAssignment.Service.Data
{
    public class QueryDbContext : DbContext
    {
        public QueryDbContext(DbContextOptions<QueryDbContext> options) : base(options) { }

        public DbSet<TenantResource> Tenants { get; set; } = null!;

        public DbSet<UserResource> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TenantResourceConfiguration());
            modelBuilder.ApplyConfiguration(new UserResourceConfiguration());
        }
    }
}
