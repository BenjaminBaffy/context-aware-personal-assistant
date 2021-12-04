using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Assistant.Domain;
using Assistant.Domain.ViewModels;

namespace Assistant.Application.Interfaces
{
    public interface IRasaHttpService
    {
        Task<BotResponseViewModel> PostMessageToBotAsync(BotMessageViewModel botMessage, CancellationToken cancellationToken);
        Task<IEnumerable<Slot>> GetTrackerSlotsAsync(string userId, CancellationToken cancellationToken);
        Task<bool> SaveSlotsAsnyc(string userId, IEnumerable<Slot> slots, CancellationToken cancellation);
        Task<HttpResponseMessage> GetHealthCheckAsync(CancellationToken cancellationToken);
    }
}