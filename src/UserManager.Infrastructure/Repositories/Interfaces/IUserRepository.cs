using UserManager.Domain.Entities;

namespace UserManager.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IQueryable<User?> GetAllActive();

        Task<User?> GetByUsernameAsync(string username);

        Task<User?> GetByEmailAsync(string email);
    }
}