using Microsoft.EntityFrameworkCore;
using SilverNetJsonApiAssignment.DAL.Data;
using SilverNetJsonApiAssignment.DAL.Entities;

namespace SilverNetJsonApiAssignment.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {

        private SilverNetJsonApiAssignmentContext _dbContext;

        public UserRepository(SilverNetJsonApiAssignmentContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<long> CreateUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);

            await _dbContext.SaveChangesAsync();

            return user.Id;
        }

        public async Task DeleteUserAsync(long userId)
        {
            User? user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with id {userId} was not found.");
            }

            _dbContext.Users.Remove(user);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsersAsync(long tenantId)
        {
            return await _dbContext.Users.Include(u => u.Tenant).Where(u => u.Tenant.Id == tenantId).ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(long userId)
        {
            return await _dbContext.Users.Include(u => u.Tenant).FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task UpdateUserAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
