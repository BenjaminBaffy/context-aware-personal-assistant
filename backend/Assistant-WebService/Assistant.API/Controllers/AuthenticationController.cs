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
            var user = await _userService.GetByPasswordAsync(request.UserName, request.Password, cancellationToken);

            if (user == null)
                return BadRequest("Incorrect user name or password");

            var accessToken = _userService.LoginUser(user, cancellationToken);

            var loginResponse = new PasswordLoginResponseViewModel
            {
                AccessToken = accessToken
            };

            return Ok(loginResponse);
        }
    }
}
