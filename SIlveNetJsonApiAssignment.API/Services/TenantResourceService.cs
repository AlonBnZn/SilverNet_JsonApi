using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Errors;
using JsonApiDotNetCore.Middleware;
using JsonApiDotNetCore.Queries;
using JsonApiDotNetCore.Repositories;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Serialization.Objects;
using JsonApiDotNetCore.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SilveNetJsonApiAssignment.Service.Data;
using SilveNetJsonApiAssignment.Service.Extantions;
using SilveNetJsonApiAssignment.Service.Resources;
using SilverNetJsonApiAssignment.Entities;
using SilverNetJsonApiAssignment.Service.Repositories;
using System.Net;

namespace SilveNetJsonApiAssignment.Service.Services
{
    public class TenantResourceService : JsonApiResourceService<TenantResource, long>
    {
        private ITenantRepository _tenantRepository;

        private ILogger<TenantResourceService> _logger;

        private CommandDbContext _dbContext;

        public TenantResourceService(IResourceRepositoryAccessor repositoryAccessor, IQueryLayerComposer queryLayerComposer, IPaginationContext paginationContext, IJsonApiOptions options, ILoggerFactory loggerFactory, IJsonApiRequest request, IResourceChangeTracker<TenantResource> resourceChangeTracker, IResourceDefinitionAccessor resourceDefinitionAccessor, ITenantRepository tenantRepository, ILogger<TenantResourceService> logger, CommandDbContext dbContext) : base(repositoryAccessor, queryLayerComposer, paginationContext, options, loggerFactory, request, resourceChangeTracker, resourceDefinitionAccessor)
        {
            _logger = logger;

            _tenantRepository = tenantRepository;

            _dbContext = dbContext;
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

                Tenant? tenant = await _dbContext.Tenants.FirstOrDefaultAsync(t => t.Id.Equals(id));

                if (tenant is null)
                {
                    _logger.LogError("Tenant not found");

                    throw new Exception("Tenant not found");
                }

                if (!string.IsNullOrWhiteSpace(resource.Name) && !resource.Name.Equals(tenant.Name))
                {
                    tenant.SetName(resource.Name);
                }

                if (!string.IsNullOrWhiteSpace(resource.Phone) && !resource.Phone.Equals(tenant.Phone))
                {
                    tenant.SetPhone(resource.Phone);
                }

                if (!string.IsNullOrWhiteSpace(resource.Email) && !resource.Email.Equals(tenant.Email))
                {
                    tenant.SetEmail(resource.Email);
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Updating tenant failed :" + ex.Message);

                throw new Exception("Error updating tenant", ex);
            }

            return null;
        }

        public override async Task DeleteAsync(long id, CancellationToken cancellationToken)
        {

            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                _logger.LogInformation("Deleting tenant...");

                var usersToDelete = await _dbContext.Users
                    .Where(u => u.Tenant.Id == id)
                    .ToListAsync(cancellationToken);

                _dbContext.Users.RemoveRange(usersToDelete);

                await _dbContext.SaveChangesAsync(cancellationToken);

                var parameter = new SqlParameter("@TenantId", id);

                var affectedRows = await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC DeleteTenantById @TenantId",
                    [parameter],
                    cancellationToken
                );

                if (affectedRows == 0)
                {
                    throw new JsonApiException(new ErrorObject(HttpStatusCode.NotFound)
                    {
                        Title = "Not Found",
                        Detail = $"Tenant with ID {id} not found or already deleted."
                    });
                }

                await transaction.CommitAsync(cancellationToken);

                _logger.LogInformation("Finished Deleting tenant:{id}", id);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                _logger.LogError("Deleting tenant failed :" + ex.Message);

                throw new Exception("Error Deleting tenant", ex);
            }
        }
    }
}
