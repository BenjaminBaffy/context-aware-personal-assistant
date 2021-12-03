using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Assistant.Application.Interfaces;
using Assistant.Domain.DatabaseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Assistant.API.Controllers.Admin
{
    [Route("api/admin/users")]
    // [OpenApiIgnore]
    [AllowAnonymous]
    public class AdminUserSlotsRestController : Controller
    {
        private readonly IRasaHttpService _rasaHttpService;
        private readonly ICurrentUserAccessor _currentUserAccessor;
        private readonly IDatabaseService<UserSlot> _userSlotDbService;

        public AdminUserSlotsRestController(
            IRasaHttpService rasaHttpService,
            ICurrentUserAccessor currentUserAccessor,
            IDatabaseService<UserSlot> userSlotDbService
        )
        {
            _rasaHttpService = rasaHttpService;
            _currentUserAccessor = currentUserAccessor;
            _userSlotDbService = userSlotDbService;
        }

        [HttpGet("{userId}/slots")]
        public async Task<IActionResult> GetAllAsync(string userId, CancellationToken cancellationToken)
        {
            var result = await _rasaHttpService.GetTrackerSlotsAsync(userId, cancellationToken);

            return Ok(result);
        }

        // TODO: this will be, other types of addition to this will be available on the FE
        [HttpGet("{userId}/slots/{slotId}")]
        public async Task<IActionResult> GetByUserIdAsync(string userId, string slotId, CancellationToken cancellationToken)
        {
            await Task.Yield();

            return Ok();
        }

        [HttpPost("{userId}/slots/single")]
        public async Task<IActionResult> AddSingleAsync(string userId, string slotName, string value, CancellationToken cancellationToken)
        {
            var existingSlots = await _userSlotDbService.QueryRecords(() =>
            {
                var query = _userSlotDbService.CollectionReference().WhereEqualTo(nameof(UserSlot.Key), slotName);
                return query;
            }, cancellationToken);

            var existingSlot = existingSlots.FirstOrDefault();

            if (existingSlot == null)
            {
                await _userSlotDbService.Add(new UserSlot
                {
                    UserId = userId,
                    Key = slotName,
                    Value = value
                });
            }
            else
            {
                existingSlot.Value = value;
                await _userSlotDbService.Update(existingSlot);
            }

            return NoContent();
        }

        [HttpPost("{userId}/slots/list")]
        public async Task<IActionResult> AddListAsync(string userId, string slotName, IEnumerable<string> values, CancellationToken cancellationToken)
        {
            var existingSlots = await _userSlotDbService.QueryRecords(() =>
            {
                var query = _userSlotDbService.CollectionReference().WhereEqualTo(nameof(UserSlot.Key), slotName);
                return query;
            }, cancellationToken);

            var existingSlot = existingSlots.FirstOrDefault();

            if (existingSlot == null)
            {
                await _userSlotDbService.Add(new UserSlot
                {
                    UserId = userId,
                    Key = slotName,
                    Values = values
                });
            }
            else
            {
                existingSlot.Values = values;
                await _userSlotDbService.Update(existingSlot);
            }

            return NoContent();
        }
    }
}
