
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Assistant.API.Controllers.Admin
{
    [Route("api/admin/users")]
    [OpenApiIgnore]
    [AllowAnonymous]
    public class AdminUserSlotsRestController : Controller
    {

        [HttpGet("{userId}/slots")]
        public async Task<IActionResult> GetAll(int userId)
        {
            await Task.Yield();

            return Ok();
        }

        // TODO: this will be, other types of addition to this will be available on the FE
        [HttpGet("{userId}/slots/{slotId}")]
        public async Task<IActionResult> GetByUserId(int userId, int slotId, CancellationToken cancellationToken)
        {
            await Task.Yield();

            return Ok();
        }
    }
}
