using Assistant.Domain.DatabaseModel;
using Assistant.Domain.ViewModels;

namespace Assistant.Application.Interfaces.Authentication
{
    public interface ITokenBuilder
    {
        AccessToken LoginAcccessToken(string encryptedBase64AccessToken);
        string BuildAccessToken(User user);
    }
}
