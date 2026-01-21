using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Middleware;
using JsonApiDotNetCore.Queries;
using JsonApiDotNetCore.Repositories;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Services;
using SilveNetJsonApiAssignment.Service.Resources;
using SilveNetJsonApiAssignment.Service.Extantions;
using SilveNetJsonApiAssignment.Service.ResourceValidations;
using SilverNetJsonApiAssignment.Entities;
using SilverNetJsonApiAssignment.Service.Repositories;

namespace SilveNetJsonApiAssignment.Service.Services
{
    public class TenantResourceService : JsonApiResourceService<TenantResource, long>
    {
        private ITenantRepository _tenantRepository;

        private ILogger<TenantResourceService> _logger;


        public TenantResourceService(IResourceRepositoryAccessor repositoryAccessor, IQueryLayerComposer queryLayerComposer, IPaginationContext paginationContext, IJsonApiOptions options, ILoggerFactory loggerFactory, IJsonApiRequest request, IResourceChangeTracker<TenantResource> resourceChangeTracker, IResourceDefinitionAccessor resourceDefinitionAccessor, ITenantRepository tenantRepository, ILogger<TenantResourceService> logger) : base(repositoryAccessor, queryLayerComposer, paginationContext, options, loggerFactory, request, resourceChangeTracker, resourceDefinitionAccessor)
        {
            _logger = logger;

            _tenantRepository = tenantRepository;

        }

        public override async Task<TenantResource?> CreateAsync(TenantResource resource, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating tenant...");

                Tenant tenant = new Tenant(resource.Name, resource.Email, resource.Phone);

                await _tenantRepository.CreateTenantAsync(tenant);

                _logger.LogInformation("Finished Creating tenant :{tenantId}", tenant.Id);

                return tenant.ToResource();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating tenant");

                throw new Exception("Error creating tenant", ex);
            }
        }

        public override async Task<TenantResource?> UpdateAsync(long id, TenantResource resource, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating tenant with id: {id}", id);

                Tenant? tenant = await _tenantRepository.GetTenantByIdAsync(id);

                if (tenant is null)
                {
                    _logger.LogError("Tenant not found");

                    throw new Exception("Tenant not found");
                }

                if (!resource.Name.Equals(null) && !resource.Name.Equals(tenant.Name) )
                {
                    tenant.SetName(resource.Name);
                }

                if (!resource.Phone.Equals(null) && !resource.Phone.Equals(tenant.Phone))
                {
                    tenant.SetPhone(resource.Phone);
                }

                if (!resource.Email.Equals(null)  && !resource.Email.Equals(tenant.Email))
                {
                    tenant.SetEmail(resource.Email);
                }

                await _tenantRepository.UpdateTenantAsync(tenant);

                return tenant.ToResource();
            }
            catch (Exception ex)
            {
                _logger.LogError("Updating tenant failed :" + ex.Message);

                throw new Exception("Error updating tenant", ex);
            }
        }

        public override async Task DeleteAsync(long id, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Deleting tenant...");

                await _tenantRepository.DeleteTenantAsync(id);

                _logger.LogInformation("Finished Deleting tenant:{id}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Deleting tenant failed :" + ex.Message);

                throw new Exception("Error Deleting tenant", ex);
            }
        }
    }
}
