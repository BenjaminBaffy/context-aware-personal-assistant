using Assistant.Domain.DatabaseModel;

namespace Assistant.Application.Interfaces.Authentication
{
    // public interface ITokenService
    // {
    //     TokenInformationResponse LoginAccessToken(string accessToken); // not needed
    //     Task<AuthenticationModel> RefreshToken(string encryptedRefreshToken, CancellationToken cancellationToken); // not needed
    // }

    public interface ITokenBuilder
    {
        // Task <(string AccessToken, string RefreshToken, long ExpiresAt)> CreateNewTokensForUser(User user, CancellationToken cancellationToken);
        string BuildAccessToken(User user);
    }
}
