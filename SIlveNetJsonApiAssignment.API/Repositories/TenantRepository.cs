using Microsoft.EntityFrameworkCore;
using SilveNetJsonApiAssignment.Service.Data;
using SilverNetJsonApiAssignment.Entities;

namespace SilverNetJsonApiAssignment.Service.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private CommandDbContext _dbContext;

        public TenantRepository(CommandDbContext commandDbContext)
        {
            _dbContext = commandDbContext;
        }

        public async Task CreateTenantAsync(Tenant tenant)
        {
            await _dbContext.Tenants.AddAsync(tenant);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTenantAsync(long tenantId)
        {
            Tenant? tenantToRemove = await _dbContext.Tenants.FirstAsync(t => t.Id.Equals(tenantId));

            _dbContext.Tenants.Remove(tenantToRemove);

            await _dbContext.SaveChangesAsync();
        }
    }
}
