using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Middleware;
using JsonApiDotNetCore.Queries;
using JsonApiDotNetCore.Repositories;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Services;
using SIlveNetJsonApiAssignment.API.Resources;
using SilverNetJsonApiAssignment.API.BLL.Mappers;
using SilverNetJsonApiAssignment.API.Validations;
using SilverNetJsonApiAssignment.DAL.Entities;
using SilverNetJsonApiAssignment.DAL.Repositories;

namespace SIlveNetJsonApiAssignment.API.Services
{
    public class TenantResourceService : JsonApiResourceService<TenantResource, long>
    {
        private ITenantRepository _tenantRepository;

        private ILogger<TenantResourceService> _logger;

        private ITenantValidation _tenantValidation;

        public TenantResourceService(IResourceRepositoryAccessor repositoryAccessor, IQueryLayerComposer queryLayerComposer, IPaginationContext paginationContext, IJsonApiOptions options, ILoggerFactory loggerFactory, IJsonApiRequest request, IResourceChangeTracker<TenantResource> resourceChangeTracker, IResourceDefinitionAccessor resourceDefinitionAccessor, ITenantRepository tenantRepository, ILogger<TenantResourceService> logger, ITenantValidation tenantValidation) : base(repositoryAccessor, queryLayerComposer, paginationContext, options, loggerFactory, request, resourceChangeTracker, resourceDefinitionAccessor)
        {
            _logger = logger;

            _tenantRepository = tenantRepository;

            _tenantValidation = tenantValidation;
        }

        public override async Task<TenantResource?> CreateAsync(TenantResource resource, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating tenant...");

                _tenantValidation.ValidateTenant(resource.Name, resource.Phone, resource.Email);

                Tenant tenant = new Tenant(resource.Name, resource.Phone, resource.Email);

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

                _tenantValidation.ValidateTenant(resource.Name, resource.Phone, resource.Email);

                Tenant? tenant = await _tenantRepository.GetTenantByIdAsync(id);

                if (tenant is null)
                {
                    _logger.LogError("Tenant not found");

                    throw new Exception("Tenant not found");
                }

                if (resource.Name != null && resource.Name != tenant.Name)
                {
                    _tenantValidation.ValidateName(resource.Name);
                    tenant.SetName(resource.Name);
                }

                if (resource.Phone != null && resource.Phone != tenant.Phone)
                {
                    _tenantValidation.ValidatePhone(resource.Phone);
                    tenant.SetPhone(resource.Phone);
                }

                if (resource.Email != null && resource.Email != tenant.Email)
                {
                    _tenantValidation.ValidatePhone(resource.Email);
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
