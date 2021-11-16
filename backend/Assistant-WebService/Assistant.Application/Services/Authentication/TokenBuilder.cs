using System;
using System.Text;
using System.Text.Json;
using Assistant.Application.Interfaces.Authentication;
using Assistant.Domain.Configuration;
using Assistant.Domain.DatabaseModel;
using Assistant.Domain.ViewModels;
using Microsoft.Extensions.Options;

namespace Assistant.Application.Services.Authentication
{
    public class TokenBuilder : ITokenBuilder
    {
        private readonly IEncryption _encryption;
        private readonly AuthenticationConfiguration _authenticationConfiguration;

        public TokenBuilder(
            IEncryption encryption,
            IOptions<AuthenticationConfiguration> authenticationConfiguration
        )
        {
            _encryption = encryption;
            _authenticationConfiguration = authenticationConfiguration.Value;
        }
        // public (string AccessToken, long ExpiresAt) BuildAccessToken(User user, DateTimeOffset now)
        // {
        //     throw new NotImplementedException();
        // }

        // public Task<(string AccessToken, string RefreshToken, long ExpiresAt)> CreateNewTokensForUser(User user, CancellationToken cancellationToken)
        // {
        //     throw new NotImplementedException();
        // }

        public string BuildAccessToken(User user)
        {
            var expiresAt = DateTimeOffset.UtcNow + TimeSpan.FromMinutes(_authenticationConfiguration.AccessTokenLifetimeInMinutes);

            var token = new AccessToken
            {
                UserId = user.Id,
                UserName = user.UserName,
                ExpirestAt = expiresAt
            };

            var serialized = JsonSerializer.Serialize<AccessToken>(token);

            return Convert.ToBase64String(_encryption.Encrypt(Encoding.UTF8.GetBytes(serialized)));
        }
    }
}