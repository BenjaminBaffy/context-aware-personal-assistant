using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Assistant.API.Controllers
{
    // TODO: not sure it's needed
    [ApiController]
    [Route("api/authentication")]
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(
            ILogger<AuthenticationController> logger
        )
        {
            _logger = logger;
        }
    }
}