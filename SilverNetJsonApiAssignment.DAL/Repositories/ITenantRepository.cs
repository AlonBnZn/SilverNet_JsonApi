using SilverNetJsonApiAssignment.DAL.Entities;

namespace SilverNetJsonApiAssignment.DAL.Repositories
{
    public interface ITenantRepository
    {
        public Task<long> CreateTenantAsync(Tenant tenant);

        public Task<Tenant?> GetTenantByIdAsync(long tenantId);

        public Task<List<Tenant>> GetAllTenantsAsync();

        public Task UpdateTenantAsync(Tenant tenant);

        public Task DeleteTenantAsync(long tenantId);
    }
}
