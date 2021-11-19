using System.Net.Http;
using System.Threading.Tasks;
using Assistant.Application.Interfaces;
using Assistant.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Assistant.API.Controllers
{
    [ApiController]
    [Route("api/rasa")]
    [Authorize]
    public class RasaController : Controller
    {
        private readonly ILogger<RasaController> _logger;
        private readonly IRasaHttpService _rasaHttpService;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public RasaController(
            ILogger<RasaController> logger,
            IRasaHttpService rasaHttpService,
            ICurrentUserAccessor currentUserAccessor
        )
        {
            _logger = logger;
            _rasaHttpService = rasaHttpService;
            _currentUserAccessor = currentUserAccessor;
        }

        [Authorize]
        [HttpPost("sendmessage")]
        [ProducesResponseType(typeof(BotResponseViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<BotResponseViewModel>> SendMessage([FromBody] BotMessageViewModel botMessage)
        {
            botMessage.Sender = _currentUserAccessor.UserId;

            var result = await _rasaHttpService.PostMessageToBot(botMessage);

            result.Recipient = _currentUserAccessor.FullName;

            return Ok(result);
        }

        [HttpGet("health")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<HttpResponseMessage>> HealthCheck()
        {
            var response = await _rasaHttpService.GetHealthCheck();

            return StatusCode((int)response.StatusCode, response);
        }
    }
}
