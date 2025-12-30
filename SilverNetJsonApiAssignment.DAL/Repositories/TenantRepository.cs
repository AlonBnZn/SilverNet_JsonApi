using Microsoft.EntityFrameworkCore;
using SilverNetJsonApiAssignment.DAL.Data;
using SilverNetJsonApiAssignment.DAL.Entities;

namespace SilverNetJsonApiAssignment.DAL.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private SilverNetJsonApiAssignmentContext _DBContext;

        public TenantRepository(SilverNetJsonApiAssignmentContext silverNetAssignmentContext)
        {
            _DBContext = silverNetAssignmentContext;
        }
        public async Task<long> CreateTenantAsync(Tenant tenant)
        {
            await _DBContext.Tenants.AddAsync(tenant);

            await _DBContext.SaveChangesAsync();

            return tenant.Id;
        }

        public async Task DeleteTenantAsync(long tenantId)
        {
            Tenant? tenantToRemove = await _DBContext.Tenants.FirstAsync(t => t.Id == tenantId);

            if (tenantToRemove == null)
            {
                throw new KeyNotFoundException($"Tenant with id {tenantId} was not found.");
            }

            _DBContext.Tenants.Remove(tenantToRemove);

            await _DBContext.SaveChangesAsync();
        }

        public async Task<List<Tenant>> GetAllTenantsAsync()
        {
            return await _DBContext.Tenants.ToListAsync();
        }

        public async Task<Tenant?> GetTenantByIdAsync(long tenantId)
        {
            Tenant? tenant = await _DBContext.Tenants
      .FirstOrDefaultAsync(t => t.Id == tenantId);

            if (tenant is null)
            {
                throw new KeyNotFoundException($"Tenant with id {tenantId} was not found.");
            }

            return tenant;
        }

        public async Task UpdateTenantAsync(Tenant tenant)
        {
            _DBContext.Tenants.Update(tenant);
            await _DBContext.SaveChangesAsync();
        }
    }
}
