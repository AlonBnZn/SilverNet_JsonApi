using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Errors;
using JsonApiDotNetCore.Middleware;
using JsonApiDotNetCore.Queries;
using JsonApiDotNetCore.Repositories;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Serialization.Objects;
using JsonApiDotNetCore.Services;
using Microsoft.EntityFrameworkCore;
using SilveNetJsonApiAssignment.Service.Data;
using SilveNetJsonApiAssignment.Service.Extantions;
using SilveNetJsonApiAssignment.Service.Resources;
using SilverNetJsonApiAssignment.Entities;
using SilverNetJsonApiAssignment.Service.Repositories;

namespace SilveNetJsonApiAssignment.Service.Services
{
    public class UserResourceService : JsonApiResourceService<UserResource, long>
    {
        private IUserRepository _userRepository;

        private ITenantRepository _tenantRepository;

        private ILogger<UserResourceService> _logger;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private CommandDbContext _dbContext;

        public UserResourceService(IResourceRepositoryAccessor repositoryAccessor, IQueryLayerComposer queryLayerComposer, IPaginationContext paginationContext, IJsonApiOptions options, ILoggerFactory loggerFactory, IJsonApiRequest request, IResourceChangeTracker<UserResource> resourceChangeTracker, IResourceDefinitionAccessor resourceDefinitionAccessor, IUserRepository userRepository, ITenantRepository tenantRepository, ILogger<UserResourceService> logger, IHttpContextAccessor httpContextAccessor, ITargetedFields targetedFields, CommandDbContext dbContext) : base(repositoryAccessor, queryLayerComposer, paginationContext, options, loggerFactory, request, resourceChangeTracker, resourceDefinitionAccessor)
        {
            _logger = logger;

            _userRepository = userRepository;

            _tenantRepository = tenantRepository;

            _httpContextAccessor = httpContextAccessor;

            _dbContext = dbContext;
        }

        public override async Task<UserResource?> CreateAsync(UserResource resource, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating user...");

                long.TryParse(_httpContextAccessor.HttpContext!.Request.RouteValues["tenantId"]?.ToString(), out long tenantId);

                Tenant? tenant = await _dbContext.Tenants.FirstOrDefaultAsync(t => t.Id.Equals(tenantId));

                if (tenant is null)
                {
                    _logger.LogError("Tenant not found");

                    throw new JsonApiException(new ErrorObject(System.Net.HttpStatusCode.UnprocessableEntity)
                    {
                        Title = "Tenant not found",
                        Detail = $"Tenant with id {tenantId} does not exist"
                    });
                }

                User user = new User(resource.FirstName, resource.LastName, resource.Phone, resource.Email, resource.IdNumber, tenant);

                await _userRepository.CreateUserAsync(user);

                _logger.LogInformation("Finished Creating user :{userId}", user.Id);

                return user.ToResource();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");

                throw new Exception("Error creating user", ex);
            }
        }
        public override async Task<UserResource?> UpdateAsync(long id, UserResource resource, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating user with id: {id}", id);

                User? user = await _dbContext.Users.FirstOrDefaultAsync(t => t.Id.Equals(id));

                if (user is null)
                {
                    _logger.LogError("User not found");

                    throw new Exception("User not found");
                }

                long.TryParse(_httpContextAccessor.HttpContext!.Request.RouteValues["tenantId"]?.ToString(), out long tenantId);

                Tenant? tenant = await _dbContext.Tenants.Include(t => t.Users).FirstOrDefaultAsync(t => t.Id.Equals(tenantId));

                if (tenant is null)
                {
                    _logger.LogError("Tenant not found");
                    throw new Exception("Tenant not found");
                }

                if (tenant.Users.FirstOrDefault(u => u.Id.Equals(user.Id)) is null)
                {
                    _logger.LogError("Forbidden - User does not belong to tenant");
                    throw new JsonApiException(new ErrorObject(System.Net.HttpStatusCode.Forbidden)
                    {
                        Title = "Forbidden",
                        Detail = "You do not have access to this resource"
                    });
                }

                if (!string.IsNullOrWhiteSpace(resource.FirstName) && !resource.FirstName.Equals(user.FirstName))
                {
                    user.SetFirstName(resource.FirstName);
                }

                if (!string.IsNullOrWhiteSpace(resource.LastName) && !resource.LastName.Equals(user.LastName))
                {
                    user.SetLastName(resource.LastName);
                }

                if (!string.IsNullOrWhiteSpace(resource.Phone) && !resource.Phone.Equals(user.Phone))
                {
                    user.SetPhone(resource.Phone);
                }

                if (!string.IsNullOrWhiteSpace(resource.Email) && !resource.Email.Equals(user.Email))
                {
                    user.SetEmail(resource.Email);
                }

                if (!string.IsNullOrWhiteSpace(resource.IdNumber) && !resource.IdNumber.Equals(user.IdNumber))
                {
                    user.SetIdNumber(resource.IdNumber);
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Updating user failed :" + ex.Message);

                throw new Exception("Error updating user", ex);
            }

            return null;
        }

        public override async Task DeleteAsync(long id, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Deleting user...");

                long.TryParse(_httpContextAccessor.HttpContext!.Request.RouteValues["tenantId"]?.ToString(), out long tenantId);

                Tenant? tenant = await _dbContext.Tenants.Include(t => t.Users).FirstOrDefaultAsync(t => t.Id.Equals(tenantId));

                if (tenant is null)
                {
                    _logger.LogError("Tenant not found");
                    throw new Exception("Tenant not found");
                }

                if (tenant.Users.FirstOrDefault(u => u.Id.Equals(id)) is null)
                {
                    _logger.LogError("Forbidden - User does not belong to tenant");
                    throw new JsonApiException(new ErrorObject(System.Net.HttpStatusCode.Forbidden)
                    {
                        Title = "Forbidden",
                        Detail = "You do not have access to this resource"
                    });
                }

                await _userRepository.DeleteUserAsync(id);

                _logger.LogInformation("Finished Deleting user:{id}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Deleting user failed :" + ex.Message);

                throw new Exception("Error Deleting user", ex);
            }
        }
    }
}