using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;
using SilveNetJsonApiAssignment.Service.ResourceValidations;
using System.ComponentModel.DataAnnotations;

namespace SilveNetJsonApiAssignment.Service.Resources
{
    [Resource(PublicName = "tenants",
              GenerateControllerEndpoints = JsonApiEndpoints.None)]
    public class TenantResource : Identifiable<long>
    {
        [Attr(PublicName ="name", 
              Capabilities = AttrCapabilities.AllowFilter | AttrCapabilities.AllowCreate | AttrCapabilities.AllowSort | AttrCapabilities.AllowView | AttrCapabilities.AllowChange)]
        [StringValidationAttribute(20, "name")]
        public string Name { get; protected set; } = null!;

        [Attr(PublicName = "email",
              Capabilities = AttrCapabilities.AllowFilter | AttrCapabilities.AllowCreate | AttrCapabilities.AllowSort | AttrCapabilities.AllowView | AttrCapabilities.AllowChange)]
        [EmailValidationAttribute]
        [StringValidationAttribute(50, "email")]
        public string Email { get; protected set; } = null!;

        [Attr(PublicName = "phone",
              Capabilities = AttrCapabilities.AllowFilter | AttrCapabilities.AllowCreate | AttrCapabilities.AllowSort | AttrCapabilities.AllowView | AttrCapabilities.AllowChange)]
        [PhoneValidationAttribute]
        [StringValidationAttribute(12, "phone")]
        public string Phone { get; protected set; } = null!;

        [Attr(PublicName = "creationDate",
              Capabilities = AttrCapabilities.AllowFilter | AttrCapabilities.AllowCreate | AttrCapabilities.AllowSort | AttrCapabilities.AllowView)]
        public DateTime CreationDate { get; protected set; }

        [HasMany] 
        public List<UserResource> Users { get; set; } = new List<UserResource>();

        public TenantResource() { }

    }
}