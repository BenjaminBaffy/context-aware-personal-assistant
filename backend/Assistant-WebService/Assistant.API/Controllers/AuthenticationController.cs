using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Assistant.Application.Interfaces;
using Assistant.Application.Interfaces.Authentication;
using Assistant.Domain;
using Assistant.Domain.DatabaseModel;
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
        private readonly IRasaHttpService _rasaHttpService;
        private readonly IDatabaseService<UserSlot> _userSlotDbService;
        private readonly ISlotDifferenceCalculator _slotDifferenceCalculator;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(
            IUserService userService,
            IRasaHttpService rasaHttpService,
            IDatabaseService<UserSlot> userSlotDbService,
            ISlotDifferenceCalculator slotDifferenceCalculator,
            ILogger<AuthenticationController> logger
        )
        {
            _userService = userService;
            _rasaHttpService = rasaHttpService;
            _userSlotDbService = userSlotDbService;
            _slotDifferenceCalculator = slotDifferenceCalculator;
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

            // Load slots from the DB
            var userSlots = await _userSlotDbService.QueryRecords(() => 
            {
                var query = _userSlotDbService.CollectionReference().WhereEqualTo(nameof(UserSlot.UserId), user.Id);
                return query;
            }, cancellationToken);

            // Load slots from Rasa
            var slots = await _rasaHttpService.GetTrackerSlotsAsync(user.Id, cancellationToken);

            // Calculate difference
            var differences = _slotDifferenceCalculator.CalculateDifferencesForRasa(userSlots, slots).ToList();
            _logger.LogInformation($"Found {differences.Count} differences");

            if (differences.Count > 0)
            {
                // Save to Rasa
                await _rasaHttpService.SaveSlotsAsnyc(user.Id, differences, cancellationToken);
            }

            var loginResponse = new PasswordLoginResponseViewModel
            {
                AccessToken = accessToken,
                FullName = user.FullName,
            };

            return Ok(loginResponse);
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // workaround
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            // TODO: do we need to do something explicitly?
            // TODO: remove slots from actual RASA?

            await Task.Yield();

            return NoContent();
        }

        private IEnumerable<Slot> a()
        {
            return null;
        }
    }
}
