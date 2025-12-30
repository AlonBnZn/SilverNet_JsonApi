using SilverNetJsonApiAssignment.DAL.Entities;

namespace SilverNetJsonApiAssignment.DAL.Repositories
{
    public interface IUserRepository
    {
        public Task<long> CreateUserAsync(User user);

        public Task<User?> GetUserByIdAsync(long userId);

        public Task<List<User>> GetAllUsersAsync(long tenantId);

        public Task UpdateUserAsync(User user);

        public Task DeleteUserAsync(long userId);
    }
}
