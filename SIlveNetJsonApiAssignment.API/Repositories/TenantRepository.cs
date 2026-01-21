using Microsoft.EntityFrameworkCore;
using SilveNetJsonApiAssignment.Service.Data;
using SilverNetJsonApiAssignment.Data;
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
        public async Task<long> CreateTenantAsync(Tenant tenant)
        {
            await _dbContext.Tenants.AddAsync(tenant);

            await _dbContext.SaveChangesAsync();

            return tenant.Id;
        }

        public async Task DeleteTenantAsync(long tenantId)
        {
            Tenant? tenantToRemove = await _dbContext.Tenants.FirstAsync(t => t.Id == tenantId);

            _dbContext.Tenants.Remove(tenantToRemove);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Tenant>> GetAllTenantsAsync()
        {
            return await _dbContext.Tenants.ToListAsync();
        }

        public async Task<Tenant?> GetTenantByIdAsync(long tenantId)
        {
            Tenant? tenant = await _dbContext.Tenants.FirstOrDefaultAsync(t => t.Id == tenantId);

            return tenant;
        }

        public async Task UpdateTenantAsync(Tenant tenant)
        {
            _dbContext.Tenants.Update(tenant);

            await _dbContext.SaveChangesAsync();
        }
    }
}
