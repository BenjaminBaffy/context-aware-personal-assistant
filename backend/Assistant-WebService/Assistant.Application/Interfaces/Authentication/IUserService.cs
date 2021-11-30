using System.Threading;
using System.Threading.Tasks;
using Assistant.Domain.DatabaseModel;

namespace Assistant.Application.Interfaces.Authentication
{
    public interface IUserService
    {
        Task<User> GetByPasswordAsync(string userName, string password, CancellationToken cancellationToken);
        string LoginUser(User user);
        (string Salt, string Hash) CreateSaltAndHash(string password);
    }
}
