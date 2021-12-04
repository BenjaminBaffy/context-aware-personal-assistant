using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Assistant.Application.Interfaces;
using Assistant.Domain.DatabaseModel;
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
        private readonly ISlotDifferenceCalculator _slotDifferenceCalculator;
        private readonly IDatabaseService<UserSlot> _userSlotDbService;

        public RasaController(
            ILogger<RasaController> logger,
            IRasaHttpService rasaHttpService,
            ICurrentUserAccessor currentUserAccessor,
            ISlotDifferenceCalculator slotDifferenceCalculator,
            IDatabaseService<UserSlot> userSlotDbService
        )
        {
            _logger = logger;
            _rasaHttpService = rasaHttpService;
            _currentUserAccessor = currentUserAccessor;
            _slotDifferenceCalculator = slotDifferenceCalculator;
            _userSlotDbService = userSlotDbService;
        }

        [HttpPost("sendmessage")]
        [ProducesResponseType(typeof(BotResponseViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<BotResponseViewModel>> SendMessage([FromBody] BotMessageViewModel botMessage, CancellationToken cancellationToken)
        {
            var userId = _currentUserAccessor.UserId;
            botMessage.Sender = userId;

            // Some of the slots is probably updated
            var result = await _rasaHttpService.PostMessageToBotAsync(botMessage, cancellationToken);

            // Load slots from the DB
            var userSlots = await _userSlotDbService.QueryRecords(() => 
            {
                var query = _userSlotDbService.CollectionReference().WhereEqualTo(nameof(UserSlot.UserId), userId);
                return query;
            }, cancellationToken);

            // query slots
            var rasaSlots = await _rasaHttpService.GetTrackerSlotsAsync(userId, cancellationToken);
            
            // We need to make sure that we delete empty slots?
            // Save delta
            var deltas = _slotDifferenceCalculator.CalculateDifferencesForDatabase(userSlots, rasaSlots);

            // Delete empty slots
            // TODO: one operation
            var tasks = new List<Task>();
            foreach (var delta in deltas)
            {
                if (delta.UserId == null)
                {
                    delta.UserId = userId;
                    tasks.Add(Task.Run(() => _userSlotDbService.Add(delta)));
                }
                else if (delta.Value == null && delta.Values == null)     
                    tasks.Add(Task.Run(() => _userSlotDbService.Delete(delta)));
                else
                    tasks.Add(Task.Run(() => _userSlotDbService.Update(delta)));   
            }

            await Task.WhenAll(tasks);


            // await batch.CommitAsync();
            
            result.Recipient = _currentUserAccessor.FullName;

            return Ok(result);
        }

        [HttpGet("health")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // not exactly true
        public async Task<ActionResult<HttpResponseMessage>> HealthCheck(CancellationToken cancellationToken)
        {
            var response = await _rasaHttpService.GetHealthCheckAsync(cancellationToken);

            return StatusCode((int)response.StatusCode, response);
        }
    }
}
