using SilverNetJsonApiAssignment.Entities;

namespace SilverNetJsonApiAssignment.Service.Repositories
{
    public interface IUserRepository
    {
        public Task CreateUserAsync(User user);

        public Task DeleteUserAsync(long userId);
    }
}
