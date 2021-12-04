using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Assistant.Application.Interfaces.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Assistant.API
{
    public class CustomAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IEncryption _encryption;
        private readonly ITokenBuilder _tokenBuilder;

        public CustomAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IEncryption encryption,
            ITokenBuilder tokenBuilder) : base(options, logger, encoder, clock)
        {
            _encryption = encryption;
            _tokenBuilder = tokenBuilder;
        }

        #pragma warning disable 1998 // We know what :)
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"], out var authHeader))
            {
                return AuthenticateResult.NoResult();
            }

            var base64EncryptedAccessToken = authHeader.Parameter;
            var accessToken = _tokenBuilder.LoginAcccessToken(base64EncryptedAccessToken);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, accessToken.UserId, ClaimValueTypes.String),
                new Claim(ClaimTypes.Name, accessToken.FullName, ClaimValueTypes.String),
                new Claim(ClaimTypes.Expiration, accessToken.ExpiresAt.ToString(CultureInfo.InvariantCulture), ClaimValueTypes.DateTime)
            };

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, nameof(CustomAuthenticationHandler)));
            var ticket = new AuthenticationTicket(principal,
                new AuthenticationProperties
                {
                    ExpiresUtc = accessToken.ExpiresAt
                }, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
        #pragma warning restore 1998

    }
}
