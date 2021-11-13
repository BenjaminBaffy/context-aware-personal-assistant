using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Assistant.Application.Interfaces;
using Assistant.Application.Interfaces.Authentication;
using Assistant.Domain.DatabaseModel;
using Assistant.Domain.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Assistant.API.Controllers.Admin
{
    [Route("api/admin/users")]
    // [OpenApiIgnore]
    // NOTE: REST maturity level 2.0
    // NOTE: For easier
    public class UserRestController : Controller
    {
        private readonly IDatabaseService<User> _userDbService;
        private readonly IUserService _userService;

        // TODO: extend to base class?
        public UserRestController(
            IDatabaseService<User> userDbService,
            IUserService userService
        )
        {
            _userDbService = userDbService;
            _userService = userService;
        }

        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetAll(CancellationToken cancellationToken)
        {
            var users = await _userDbService.GetAll();

            return Ok(
                users.Select(u => new UserViewModel
                {
                    Id = u.Id,
                    Password = u.Password,
                    Salt = u.Salt,
                    UserName = u.UserName
                }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewModel>> GetById(string id, CancellationToken cancellationToken)
        {
            var user = await _userDbService.Get(new User { Id = id });

            return Ok(
                new UserViewModel
                {
                    Id = user.Id,
                    Password = user.Password,
                    Salt = user.Salt,
                    UserName = user.UserName
                }
            );
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateUserViewModel user, CancellationToken cancellationToken)
        {
            var (salt, passwordHash) = _userService.CreateSaltAndHash(user.Password);

            var newUser = await _userDbService.Add(
                new User
                {
                    Password = passwordHash,
                    Salt = salt,
                    UserName = user.UserName
                }
            );
            
            return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] UpdateUserViewModel user)
        {
            user.Id = id;
            var (salt, passwordHash) = _userService.CreateSaltAndHash(user.Password);

            var success = await _userDbService.Update(
                new User
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Password = passwordHash,
                    Salt = salt,
                }
            );

            return success ? NoContent() : BadRequest("Could not update user.");
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await _userDbService.Delete(new User {Id = id});

            return success ? NoContent() : BadRequest("Could not delete user.");
        }
    }
}
