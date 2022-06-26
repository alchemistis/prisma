using Microsoft.AspNetCore.Mvc;
using Prisma.Api.Models;
using Prisma.Api.Services;

namespace Prisma.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserAuthResponse>> Post([FromBody] UserAuthRequest userAuthRequest)
        {
            var userAuthResponse = await _userService.ValidateUser(
                userAuthRequest.Username,
                userAuthRequest.Password);

            if (userAuthResponse is null)
                return Unauthorized();

            return Ok(userAuthResponse);
        }
    }
}
