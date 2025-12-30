using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;

namespace SIlveNetJsonApiAssignment.API.Resources
{
    [Resource(GenerateControllerEndpoints = JsonApiEndpoints.None)]
    public class UserResource : Identifiable<long>
    {
        [Attr] public string FirstName { get; private set; } = null!;

        [Attr] public string LastName { get; private set; } = null!;

        [Attr] public string Email { get; private set; } = null!;

        [Attr] public string Phone { get; private set; } = null!;

        [Attr] public string IdNumber { get; private set; } = null!;

        [Attr] public DateTime CreationDate { get; private set; }

        [HasOne] public TenantResource? Tenant { get; set; } = null!;

        public UserResource()
        {

        }
        public UserResource(long id, string firstName, string lastName, string phone,
                           string email, string idNumber, DateTime creationDate, TenantResource? tenant = null)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            IdNumber = idNumber;
            CreationDate = creationDate;
            Tenant = tenant;
        }
    }
}
