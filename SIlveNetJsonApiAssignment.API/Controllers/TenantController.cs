using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Controllers.Annotations;
using JsonApiDotNetCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIlveNetJsonApiAssignment.API.Resources;
using SilverNetJsonApiAssignment.API.Authorization;

namespace SIlveNetJsonApiAssignment.API.Controllers
{
    [DisableRoutingConvention]
    [Route("api/v1")]
    public class TenantController : BaseJsonApiController<TenantResource, long>
    {
        public TenantController(IJsonApiOptions options, IResourceGraph resourceGraph, ILoggerFactory loggerFactory, IResourceService<TenantResource, long> resourceService) : base(options, resourceGraph, loggerFactory, resourceService)
        {
        }

        [HttpGet("tenants")]
        [Authorize(Policy = Policies.Tenant)]
        public override Task<IActionResult> GetAsync(CancellationToken cancellationToken)
        {
            return base.GetAsync(cancellationToken);
        }

        [HttpGet("tenants/{id}")]
        [Authorize(Policy = Policies.Tenant)]
        public override Task<IActionResult> GetAsync(long id, CancellationToken cancellationToken)
        {
            return base.GetAsync(id, cancellationToken);
        }

        [HttpPost("tenants")]
        [Authorize(Policy = Policies.Tenant)]
        public override Task<IActionResult> PostAsync([FromBody] TenantResource resource, CancellationToken cancellationToken)
        {
            return base.PostAsync(resource, cancellationToken);
        }

        [HttpPatch("tenants/{id}")]
        [Authorize(Policy = Policies.Tenant)]
        public override Task<IActionResult> PatchAsync(long id, [FromBody] TenantResource resource, CancellationToken cancellationToken)
        {
            return base.PatchAsync(id, resource, cancellationToken);
        }

        [HttpDelete("tenants/{id}")]
        [Authorize(Policy = Policies.Tenant)]
        public override Task<IActionResult> DeleteAsync(long id, CancellationToken cancellationToken)
        {
            return base.DeleteAsync(id, cancellationToken);
        }
    }
}
