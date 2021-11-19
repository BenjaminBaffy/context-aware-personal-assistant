using System.Threading;
using System.Threading.Tasks;
using Assistant.Application.Interfaces.Authentication;
using Assistant.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Assistant.API.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(
            IUserService userService,
            ILogger<AuthenticationController> logger
        )
        {
            _userService = userService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(PasswordLoginResponseViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] PasswordLoginViewModel request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByPasswordAsync(request.LoginName, request.Password, cancellationToken);

            if (user == null)
                return BadRequest("Incorrect user name or password");

            var accessToken = _userService.LoginUser(user);

            // TODO: load slots, conversations, etc to rasa

            var loginResponse = new PasswordLoginResponseViewModel
            {
                AccessToken = accessToken,
                FullName = user.FullName,
            };

            return Ok(loginResponse);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            // TODO: do we need to do something explicitly?
            // TODO: remove slots from actual RASA?

            await Task.Yield();

            return NoContent();
        }
    }
}
