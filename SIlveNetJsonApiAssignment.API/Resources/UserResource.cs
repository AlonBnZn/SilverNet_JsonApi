using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;
using SilveNetJsonApiAssignment.Service.ResourceValidations;

namespace SilveNetJsonApiAssignment.Service.Resources
{
    [Resource(PublicName = "users",
              GenerateControllerEndpoints = JsonApiEndpoints.None)]
    public class UserResource : Identifiable<long>
    {
        [Attr(PublicName = "firstName",
              Capabilities = AttrCapabilities.AllowFilter | AttrCapabilities.AllowCreate | AttrCapabilities.AllowSort | AttrCapabilities.AllowView | AttrCapabilities.AllowChange)]
        [StringValidationAttribute(10, "firstName")]
        public string FirstName { get; set; } = null!;

        [Attr(PublicName = "lastName",
              Capabilities = AttrCapabilities.AllowFilter | AttrCapabilities.AllowCreate | AttrCapabilities.AllowSort | AttrCapabilities.AllowView | AttrCapabilities.AllowChange)]
        [StringValidationAttribute(10, "lastName")]
        public string LastName { get; set; } = null!;

        [Attr(PublicName = "email",
              Capabilities = AttrCapabilities.AllowFilter | AttrCapabilities.AllowCreate | AttrCapabilities.AllowSort | AttrCapabilities.AllowView | AttrCapabilities.AllowChange)]
        [EmailValidationAttribute]
        [StringValidationAttribute(50, "phone")]
        public string Email { get; set; } = null!;

        [Attr(PublicName = "phone",
              Capabilities = AttrCapabilities.AllowFilter | AttrCapabilities.AllowCreate | AttrCapabilities.AllowSort | AttrCapabilities.AllowView | AttrCapabilities.AllowChange)]
        [PhoneValidationAttribute]
        [StringValidationAttribute(12, "phone")]
        public string Phone { get; set; } = null!;

        [Attr(PublicName = "idNumber",
              Capabilities = AttrCapabilities.AllowFilter | AttrCapabilities.AllowCreate | AttrCapabilities.AllowSort | AttrCapabilities.AllowView | AttrCapabilities.AllowChange)]
        [IdNumberValidationAttribute]
        [StringValidationAttribute(9, "idNumber")]
        public string IdNumber { get; set; } = null!;

        [Attr(PublicName = "creationDate",
              Capabilities = AttrCapabilities.AllowFilter | AttrCapabilities.AllowCreate | AttrCapabilities.AllowSort | AttrCapabilities.AllowView)]
        public DateTime CreationDate { get; set; }

        [HasOne]
        public TenantResource? Tenant { get; set; } = null!;

        public UserResource() { }
    }
}
