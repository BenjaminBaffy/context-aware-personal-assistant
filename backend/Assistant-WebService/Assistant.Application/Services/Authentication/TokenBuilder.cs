using System;
using System.Text;
using System.Text.Json;
using Assistant.Application.Interfaces.Authentication;
using Assistant.Domain.Configuration;
using Assistant.Domain.DatabaseModel;
using Assistant.Domain.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Assistant.Application.Services.Authentication
{
    public class TokenBuilder : ITokenBuilder
    {
        private readonly IEncryption _encryption;
        private readonly AuthenticationConfiguration _authenticationConfiguration;
        private readonly ILogger<TokenBuilder> _logger;

        public TokenBuilder(
            IEncryption encryption,
            IOptions<AuthenticationConfiguration> authenticationConfiguration,
            ILogger<TokenBuilder> logger
        )
        {
            _encryption = encryption;
            _authenticationConfiguration = authenticationConfiguration.Value;
            _logger = logger;
        }

        public AccessToken LoginAcccessToken(string encryptedBase64AccessToken)
        {
            var json = Encoding.UTF8.GetString(_encryption.Decrypt(Convert.FromBase64String(encryptedBase64AccessToken)));
            var accessToken = TrySerializeToken<AccessToken>(json);

            if (accessToken.ExpiresAt > DateTimeOffset.UtcNow) // not expired
                return accessToken;

            throw new TokenExpiredException(accessToken.ExpiresAt);
        }

        public string BuildAccessToken(User user)
        {
            var expiresAt = DateTimeOffset.UtcNow + TimeSpan.FromMinutes(_authenticationConfiguration.AccessTokenLifetimeInMinutes);

            var token = new AccessToken
            {
                UserId = user.Id,
                FullName = user.FullName,
                ExpiresAt = expiresAt
            };

            var serialized = JsonSerializer.Serialize<AccessToken>(token);

            return Convert.ToBase64String(_encryption.Encrypt(Encoding.UTF8.GetBytes(serialized)));
        }

        private T TrySerializeToken<T>(string json)
        {
            try
            {
                var token = JsonSerializer.Deserialize<T>(json);
                return token;
            }
            catch (JsonException jex)
            {
                _logger.LogWarning(jex, jex.Message);
                throw new InvalidTokenException(json);
            }
        }
    }
}
