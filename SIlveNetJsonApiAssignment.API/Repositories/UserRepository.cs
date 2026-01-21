using Microsoft.EntityFrameworkCore;
using SilveNetJsonApiAssignment.Service.Data;
using SilverNetJsonApiAssignment.Data;
using SilverNetJsonApiAssignment.Entities;

namespace SilverNetJsonApiAssignment.Service.Repositories
{
    public class UserRepository : IUserRepository
    {
        private CommandDbContext _dbContext;

        public UserRepository(CommandDbContext commandDbContext)
        {
            _dbContext = commandDbContext;
        }

        public async Task<long> CreateUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);

            await _dbContext.SaveChangesAsync();

            return user.Id;
        }

        public async Task DeleteUserAsync(long userId)
        {
            User? userToRemove = await _dbContext.Users.FirstAsync(u => u.Id == userId);

            _dbContext.Users.Remove(userToRemove);

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
