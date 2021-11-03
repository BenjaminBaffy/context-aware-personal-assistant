using System.Net.Http;
using System.Threading.Tasks;
using Assistant.Application.Interfaces;
using Assistant.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Assistant.API.Controllers
{
    [ApiController]
    [Route("api/rasa")]
    public class RasaController : Controller
    {
        private readonly ILogger<RasaController> _logger;
        private readonly IRasaHttpService _rasaHttpService;

        public RasaController(
            ILogger<RasaController> logger,
            IRasaHttpService rasaHttpService
        )
        {
            _logger = logger;
            _rasaHttpService = rasaHttpService;
        }

        [HttpPost("sendmessage")]
        [ProducesResponseType(typeof(BotResponseViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<BotResponseViewModel>> SendMessage([FromBody] BotMessageViewModel botMessage)
        {
            var result = await _rasaHttpService.PostMessageToBot(botMessage);

            return Ok(result);
        }

        [HttpGet("health")]
        public async Task<ActionResult<HttpResponseMessage>> HealthCheck()
        {
            var response = await _rasaHttpService.GetHealthCheck();

            return StatusCode((int)response.StatusCode, response);
        }
    }
}
