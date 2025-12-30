using Microsoft.EntityFrameworkCore;
using SIlveNetJsonApiAssignment.API.Resources;
using SilverNetJsonApiAssignment.DAL.Configurations;

namespace SilverNetJsonApiAssignment.API.Data
{
    public class CommandDBContext : DbContext
    {
        public CommandDBContext(DbContextOptions<CommandDBContext> options)
     : base(options)
        {
        }
        public DbSet<TenantResource> Tenants { get; set; } = null!;
        public DbSet<UserResource> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TenantConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
