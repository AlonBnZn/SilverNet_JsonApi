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
    public class UserResourceService : JsonApiResourceService<UserResource, long>
    {
        private IUserRepository _userRepository;

        private ITenantRepository _tenantRepository;

        private ILogger<UserResourceService> _logger;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserResourceService(IResourceRepositoryAccessor repositoryAccessor, IQueryLayerComposer queryLayerComposer, IPaginationContext paginationContext, IJsonApiOptions options, ILoggerFactory loggerFactory, IJsonApiRequest request, IResourceChangeTracker<UserResource> resourceChangeTracker, IResourceDefinitionAccessor resourceDefinitionAccessor, IUserRepository userRepository, ITenantRepository tenantRepository, ILogger<UserResourceService> logger, IHttpContextAccessor httpContextAccessor, ITargetedFields targetedFields) : base(repositoryAccessor, queryLayerComposer, paginationContext, options, loggerFactory, request, resourceChangeTracker, resourceDefinitionAccessor)
        {
            _logger = logger;

            _userRepository = userRepository;

            _tenantRepository = tenantRepository;


            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task<UserResource> CreateAsync(UserResource resource, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating user...");

                long.TryParse(_httpContextAccessor.HttpContext!.Request.RouteValues["tenantId"]?.ToString(), out long tenantId);

                Tenant? tenant = await _tenantRepository.GetTenantByIdAsync(tenantId);

                if (tenant is null)
                {
                    _logger.LogError("Tenant not found");
                    throw new Exception("Tenant not found");
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
        public override async Task<UserResource> UpdateAsync(long id, UserResource resource, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating user with id: {id}", id);

                User? user = await _userRepository.GetUserByIdAsync(id);

                if (user is null)
                {
                    _logger.LogError("User not found");

                    throw new Exception("User not found");
                }

                if (!resource.FirstName.Equals(null) && !resource.FirstName.Equals(user.FirstName))
                {
                    user.SetFirstName(resource.FirstName);
                }

                if (!resource.LastName.Equals(null) && !resource.LastName.Equals(user.LastName))
                {
                    user.SetLastName(resource.LastName);
                }

                if (!resource.Phone.Equals(null) && !resource.Phone.Equals(user.Phone))
                {
                    user.SetPhone(resource.Phone);
                }

                if (!resource.Email.Equals(null) && !resource.Email.Equals(user.Email))
                {
                    user.SetEmail(resource.Email);
                }

                if (!resource.IdNumber.Equals(null) && !resource.IdNumber.Equals(user.IdNumber))
                {
                    user.SetIdNumber(resource.IdNumber);
                }

                await _userRepository.UpdateUserAsync(user);

                return user.ToResource();
            }
            catch (Exception ex)
            {
                _logger.LogError("Updating user failed :" + ex.Message);

                throw new Exception("Error updating user", ex);
            }
        }

        public override async Task DeleteAsync(long id, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Deleting user...");

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