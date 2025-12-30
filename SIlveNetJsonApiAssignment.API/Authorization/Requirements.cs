using Microsoft.AspNetCore.Authorization;

namespace SilverNetJsonApiAssignment.API.Authorization
{
    public class Requirements
    {
        public class UserRequirement : IAuthorizationRequirement { };
        public class TenantRequirement : IAuthorizationRequirement { };

    }
}
