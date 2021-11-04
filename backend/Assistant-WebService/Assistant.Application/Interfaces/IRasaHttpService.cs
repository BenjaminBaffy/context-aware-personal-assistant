using System.Net.Http;
using System.Threading.Tasks;
using Assistant.Domain.ViewModels;

namespace Assistant.Application.Interfaces
{
    public interface IRasaHttpService
    {
        Task<BotResponseViewModel> PostMessageToBot(BotMessageViewModel botMessage);
        Task<HttpResponseMessage> GetHealthCheck();
    }
}