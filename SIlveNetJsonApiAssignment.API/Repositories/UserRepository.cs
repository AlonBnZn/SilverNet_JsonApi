using Microsoft.EntityFrameworkCore;
using SilveNetJsonApiAssignment.Service.Data;
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

        public async Task CreateUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(long userId)
        {
            User? userToRemove = await _dbContext.Users.FirstAsync(u => u.Id == userId);

            _dbContext.Users.Remove(userToRemove);

            await _dbContext.SaveChangesAsync();
        }
    }
}
