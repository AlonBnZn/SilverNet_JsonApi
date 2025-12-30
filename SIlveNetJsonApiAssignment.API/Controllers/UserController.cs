using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Controllers.Annotations;
using JsonApiDotNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using SIlveNetJsonApiAssignment.API.Resources;

namespace SIlveNetJsonApiAssignment.API.Controllers
{
    [DisableRoutingConvention]
    [ApiController]
    [Route("api/v1")]
    public class UserController : BaseJsonApiController<UserResource, long>
    {
        public UserController(IJsonApiOptions options, IResourceGraph resourceGraph, ILoggerFactory loggerFactory, IResourceService<UserResource, long> resourceService) : base(options, resourceGraph, loggerFactory, resourceService)
        {
        }

        [HttpGet("tenants/{tenantId}/users")]
        public override Task<IActionResult> GetAsync(CancellationToken cancellationToken)
        {
            return base.GetAsync(cancellationToken);
        }

        [HttpGet("tenants/{tenantId}/users/{id}")]
        public override Task<IActionResult> GetAsync(long id, CancellationToken cancellationToken)
        {
            return base.GetAsync(id, cancellationToken);
        }

        [HttpPost("tenants/{tenantId}/users")]
        public override Task<IActionResult> PostAsync([FromBody] UserResource resource, CancellationToken cancellationToken)
        {
            return base.PostAsync(resource, cancellationToken);
        }

        [HttpPatch("tenants/{tenantId}/users/{id}")]
        public override Task<IActionResult> PatchAsync(long id, [FromBody] UserResource resource, CancellationToken cancellationToken)
        {
            return base.PatchAsync(id, resource, cancellationToken);
        }

        [HttpDelete("tenants/{tenantId}/users/{id}")]
        public override Task<IActionResult> DeleteAsync(long id, CancellationToken cancellationToken)
        {
            return base.DeleteAsync(id, cancellationToken);
        }
    }
}
