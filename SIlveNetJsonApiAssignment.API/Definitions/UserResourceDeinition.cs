using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Queries.Expressions;
using JsonApiDotNetCore.Resources;
using SilveNetJsonApiAssignment.Service.Resources;

namespace SilveNetJsonApiAssignment.Service.Definitions
{
    public class UserResourceDeinition : JsonApiResourceDefinition<UserResource, long>
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ILogger<UserResourceDeinition> _logger;
        public UserResourceDeinition(IResourceGraph resourceGraph, IHttpContextAccessor httpContextAccessor, ILogger<UserResourceDeinition> logger) : base(resourceGraph)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public override FilterExpression? OnApplyFilter(FilterExpression? existingFilter)
        {
            var tenantIdString = _httpContextAccessor.HttpContext!.Request.RouteValues["tenantId"]?.ToString();

            long.TryParse(tenantIdString, out var tenantId);

            var tenantIdAttribute = ResourceType.Attributes
                .Single(attr => attr.Property.Name == nameof(UserResource.Tenant.Id));

            var tenantRelationship = ResourceType.Relationships
                .Single(rel => rel.Property.Name == nameof(UserResource.Tenant));

            var tenantFilter = new ComparisonExpression(
               ComparisonOperator.Equals,
               new ResourceFieldChainExpression([tenantRelationship, tenantIdAttribute]),
               new LiteralConstantExpression(tenantId));

            return existingFilter == null
                ? (FilterExpression)tenantFilter
                : new LogicalExpression(LogicalOperator.And,
                    new[] { tenantFilter, existingFilter });
        }
    }
}
