using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;

namespace SIlveNetJsonApiAssignment.API.Resources
{
    [Resource(GenerateControllerEndpoints = JsonApiEndpoints.None)]
    public class TenantResource : Identifiable<long>
    {
        [Attr] public string Name { get; private set; } = null!;

        [Attr] public string Email { get; private set; } = null!;

        [Attr] public string Phone { get; private set; } = null!;

        [Attr] public DateTime CreationDate { get; private set; }

        [HasMany] public List<UserResource> Users { get; set; } = new List<UserResource>();

        public TenantResource() { }

        public TenantResource(long id, string name, string email, string phone, DateTime creationDate)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Email = email;
            CreationDate = creationDate;
        }
    }
}