using SIlveNetJsonApiAssignment.API.Resources;
using SilverNetJsonApiAssignment.DAL.Entities;

namespace SilverNetJsonApiAssignment.API.BLL.Mappers
{
    public static class TenantMapper
    {
        public static TenantResource ToResource(this Tenant tenant)
        {
            return new TenantResource(tenant.Id, tenant.Name, tenant.Phone, tenant.Email, tenant.CreationDate);
        }

        public static List<TenantResource> ToResourceList(this IEnumerable<Tenant> tenants)
        {
            return tenants.Select(t => t.ToResource()).ToList();
        }
    }
}
