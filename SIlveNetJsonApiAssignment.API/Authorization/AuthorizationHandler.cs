using Microsoft.AspNetCore.Authorization;
using static SilverNetJsonApiAssignment.API.Authorization.Requirements;

namespace SilverNetJsonApiAssignment.API.Authorization
{
    public class AuthorizationHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var requirements = context.PendingRequirements.ToList();

            foreach (var requirement in requirements)
            {
                if (requirement is UserRequirement)
                {
                    if (context.User.IsInRole("User"))
                    {
                        context.Succeed(requirement);
                    }
                }
                else if (requirement is TenantRequirement)
                {
                    if (context.User.IsInRole("Tenant"))
                    {
                        context.Succeed(requirement);
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
