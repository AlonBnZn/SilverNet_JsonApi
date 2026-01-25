using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;
using SilveNetJsonApiAssignment.Service.ResourceValidations;

namespace SilveNetJsonApiAssignment.Service.Resources
{
    [Resource(PublicName = "tenants",
              GenerateControllerEndpoints = JsonApiEndpoints.None)]
    public class TenantResource : Identifiable<long>
    {
        [Attr(PublicName = "name",
              Capabilities = AttrCapabilities.AllowFilter | AttrCapabilities.AllowCreate | AttrCapabilities.AllowSort | AttrCapabilities.AllowView | AttrCapabilities.AllowChange)]
        [StringValidationAttribute(20, "name")]
        public string Name { get; set; } = null!;

        [Attr(PublicName = "email",
              Capabilities = AttrCapabilities.AllowFilter | AttrCapabilities.AllowCreate | AttrCapabilities.AllowSort | AttrCapabilities.AllowView | AttrCapabilities.AllowChange)]
        [EmailValidationAttribute]
        [StringValidationAttribute(50, "email")]
        public string Email { get; set; } = null!;

        [Attr(PublicName = "phone",
              Capabilities = AttrCapabilities.AllowFilter | AttrCapabilities.AllowCreate | AttrCapabilities.AllowSort | AttrCapabilities.AllowView | AttrCapabilities.AllowChange)]
        [PhoneValidationAttribute]
        [StringValidationAttribute(12, "phone")]
        public string Phone { get; set; } = null!;

        [Attr(PublicName = "creationDate",
              Capabilities = AttrCapabilities.AllowFilter | AttrCapabilities.AllowCreate | AttrCapabilities.AllowSort | AttrCapabilities.AllowView)]
        public DateTime? CreationDate { get; set; }

        [HasMany]
        public List<UserResource> Users { get; set; } = new List<UserResource>();
    }
}