using Microsoft.AspNetCore.Authorization;

namespace SilveNetJsonApiAssignment.Service.Authorization
{
    public class Requirements
    {
        public class UserRequirement : IAuthorizationRequirement { };
        public class TenantRequirement : IAuthorizationRequirement { };

    }
}
