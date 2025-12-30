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
    public class TenantController : BaseJsonApiController<TenantResource, long>
    {
        private IResourceService<TenantResource, long> _resourceService;
        public TenantController(IJsonApiOptions options, IResourceGraph resourceGraph, ILoggerFactory loggerFactory, IResourceService<TenantResource, long> resourceService) : base(options, resourceGraph, loggerFactory, resourceService)
        {
            _resourceService = resourceService;
        }

        [HttpGet("tenants")]
        public override Task<IActionResult> GetAsync(CancellationToken cancellationToken)
        {
            return base.GetAsync(cancellationToken);
        }

        [HttpGet("tenants/{id}")]
        public override Task<IActionResult> GetAsync(long id, CancellationToken cancellationToken)
        {
            return base.GetAsync(id, cancellationToken);
        }

        [HttpPost("tenants")]
        public override Task<IActionResult> PostAsync([FromBody] TenantResource resource, CancellationToken cancellationToken)
        {
            return base.PostAsync(resource, cancellationToken);
        }

        [HttpPatch("tenants/{id}")]
        public override Task<IActionResult> PatchAsync(long id, [FromBody] TenantResource resource, CancellationToken cancellationToken)
        {
            return base.PatchAsync(id, resource, cancellationToken);
        }

        [HttpDelete("tenants/{id}")]
        public override Task<IActionResult> DeleteAsync(long id, CancellationToken cancellationToken)
        {
            return base.DeleteAsync(id, cancellationToken);
        }
    }
}
