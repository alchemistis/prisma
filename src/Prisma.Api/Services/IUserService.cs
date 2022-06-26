using Prisma.Api.Models;

namespace Prisma.Api.Services
{
    public interface IUserService
    {
        Task<UserAuthResponse?> ValidateUser(string? username, string? password);
    }
}
