using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snowdrop.Auth.Managers.JwtAuthManager;
using Snowdrop.Auth.Models;
using Snowdrop.BL.Services.Users;
using Snowdrop.Infrastructure.Dto.Users;

namespace Snowdrop.Api.Controllers
{
    [AllowAnonymous]
    [Controller]
    [Route("[controller]")]
    public sealed class AuthController : ControllerBase
    {
        private readonly IJwtAuthManager _authManager = default;
        private readonly IUserService _userService = default;

        public AuthController(
            IJwtAuthManager authManager,
            IUserService userService
        )
        {
            _authManager = authManager;
            _userService = userService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<JwtAuthResult>> SignUp([FromBody] SignUpRequest req)
        {
            var newUser = await _userService.SingUp(req);
            var authResult = await _authManager.GenerateToken(newUser.UserName, newUser.Claims);

            return Ok(authResult);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<JwtAuthResult>> SignIn([FromBody] SignInRequest req)
        {
            var user = await _userService.SingIn(req);
            var authResult = await _authManager.GenerateToken(user.UserName, user.Claims);

            return Ok(authResult);
        }

        [HttpPost]
        [Authorize]
        public new ActionResult SignOut()
        {
            _authManager.RemoveRefreshToken(User.Identity?.Name);
            return Ok();
        }
    }
}