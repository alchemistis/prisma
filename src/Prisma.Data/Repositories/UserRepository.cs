using Microsoft.EntityFrameworkCore;
using Prisma.Data.Models;

namespace Prisma.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PrismaDbContext _context;

        public UserRepository(PrismaDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _context.Users.Where(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            return await _context.Users.Where(user => user.Username == name).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
