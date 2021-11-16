using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Assistant.Application.Interfaces;
using Assistant.Application.Interfaces.Authentication;
using Assistant.Domain.DatabaseModel;
using Microsoft.Extensions.Logging;

namespace Assistant.Application.Services.Authentication
{
    public class UserService : IUserService
    {
        private readonly IDatabaseService<User> _userDbService;
        private readonly ITokenBuilder _tokenBuilder;
        private readonly Sha512Helper _sha512Helper;
        private readonly ILogger<IUserService> _logger;

        public UserService(
            IDatabaseService<User> userDbService,
            ITokenBuilder tokenBuilder,
            Sha512Helper sha512Helper,
            ILogger<IUserService> logger
        )
        {
            _userDbService = userDbService;
            _tokenBuilder = tokenBuilder;
            _sha512Helper = sha512Helper;
            _logger = logger;
        }


        public async Task<User> GetByPasswordAsync(string userName, string password, CancellationToken cancellationToken)
        {
            var users = await _userDbService.QueryRecords(() => 
            {
                var query = _userDbService.CollectionReference()
                    .WhereEqualTo(nameof(User.UserName), userName);

                return query;
            }, cancellationToken);
            var userList = users.ToList();

            var user = userList.FirstOrDefault();

            if (ComputeHash(password, user.Salt) != user.Password)
            {
                _logger.LogWarning("Wrong username or password");
                return null;
            }

            if (user == null || userList.Count > 0)
                _logger.LogWarning($"User count is :{userList.Count}");

            return user;
        }

        public string LoginUser(User user, CancellationToken cancellationToken)
        {
            var tokenString = _tokenBuilder.BuildAccessToken(user);

            return tokenString;
        }

        public (string Salt, string Hash) CreateSaltAndHash(string password)
        {
            var salt = _sha512Helper.ComputeHash(DateTimeOffset.Now.UtcTicks.ToString());
            return (salt, ComputeHash(password, salt));
        }

        private string ComputeHash(string password, string salt)
        {
            return _sha512Helper.ComputeHash($"{salt}:{password}");
        }

    }
}