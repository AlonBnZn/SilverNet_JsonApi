using SilverNetJsonApiAssignment.Entities;

namespace SilverNetJsonApiAssignment.Service.Repositories
{
    public interface ITenantRepository
    {
        public Task CreateTenantAsync(Tenant tenant);

        public Task DeleteTenantAsync(long tenantId);
    }
}
