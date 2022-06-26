using Prisma.Data.Models;

namespace Prisma.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(int id);
        Task<User> GetUserByNameAsync(string name);
        Task<IEnumerable<User>> GetUsersAsync();
    }
}
