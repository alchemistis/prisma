using Prisma.Api.Models;
using Prisma.Data.Repositories;

namespace Prisma.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenService _tokenService;

        public UserService(IUserRepository userRepository, JwtTokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<UserAuthResponse?> ValidateUser(string? username, string? password)
        {
            var user = await _userRepository.GetUserByNameAsync(username);

            if (user is null)
                return null;

            if (user.Password != password) return null;

            var token = _tokenService.CreateToken(user);
            return new UserAuthResponse(user.Username, token);
        }
    }
}
