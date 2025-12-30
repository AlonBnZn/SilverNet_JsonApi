using SIlveNetJsonApiAssignment.API.Resources;
using SilverNetJsonApiAssignment.DAL.Entities;

namespace SilverNetJsonApiAssignment.API.BLL.Mappers
{
    public static class UserMapper
    {
        public static UserResource ToResource(this User user)
        {
            return new UserResource(user.Id, user.FirstName, user.LastName, user.Phone, user.Email, user.IdNumber, user.CreationDate, user.Tenant!.ToResource());
        }

        public static List<UserResource> ToResourceList(this IEnumerable<User> users)
        {
            return users.Select(u => u.ToResource()).ToList();
        }
    }
}
