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
    public class UserResourceService : JsonApiResourceService<UserResource, long>
    {
        private IUserRepository _userRepository;

        private ITenantRepository _tenantRepository;

        private ILogger<UserResourceService> _logger;

        private IUserValidation _userValidation;

        private readonly IHttpContextAccessor _httpContextAccessor;



        public UserResourceService(IResourceRepositoryAccessor repositoryAccessor, IQueryLayerComposer queryLayerComposer, IPaginationContext paginationContext, IJsonApiOptions options, ILoggerFactory loggerFactory, IJsonApiRequest request, IResourceChangeTracker<UserResource> resourceChangeTracker, IResourceDefinitionAccessor resourceDefinitionAccessor, IUserRepository userRepository, ITenantRepository tenantRepository, ILogger<UserResourceService> logger, IUserValidation userValidation, IHttpContextAccessor httpContextAccessor, ITargetedFields targetedFields) : base(repositoryAccessor, queryLayerComposer, paginationContext, options, loggerFactory, request, resourceChangeTracker, resourceDefinitionAccessor)
        {
            _logger = logger;

            _userRepository = userRepository;

            _tenantRepository = tenantRepository;

            _userValidation = userValidation;

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

                _userValidation.ValidateUser(resource.FirstName, resource.LastName, resource.Phone, resource.Email, resource.IdNumber);

                User user = new User(resource.FirstName, resource.LastName, resource.Phone, resource.Email, resource.IdNumber);

                user.SetTenant(tenant);

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

                if (resource.FirstName != null && resource.FirstName != user.FirstName)
                {
                    _userValidation.ValidateFirstName(resource.FirstName);
                    user.SetFirstName(resource.FirstName);
                }

                if (resource.LastName != null && resource.LastName != user.LastName)
                {
                    _userValidation.ValidateLastName(resource.LastName);
                    user.SetLastName(resource.LastName);
                }

                if (resource.Phone != null && resource.Phone != user.Phone)
                {
                    _userValidation.ValidatePhone(resource.Phone);
                    user.SetPhone(resource.Phone);
                }

                if (resource.Email != null && resource.Email != user.Email)
                {
                    _userValidation.ValidateEmail(resource.Email);
                    user.SetEmail(resource.Email);
                }

                if (resource.IdNumber != null && resource.IdNumber != user.IdNumber)
                {
                    _userValidation.ValidateIdNumber(resource.IdNumber);
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

