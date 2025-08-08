using Microsoft.EntityFrameworkCore;
using UserManager.Domain.Entities;
using UserManager.Domain.Enums;
using UserManager.Infrastructure.Repositories.Interfaces;

namespace UserManager.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext context) : Repository<User>(context), IUserRepository
    {
        public IQueryable<User?> GetAllActive()
        {
            return context.Users.OrderByDescending(u => u.LastLoginDate).AsQueryable();
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Status == Status.Active);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
           return await context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Status == Status.Active);
        }
    }
}