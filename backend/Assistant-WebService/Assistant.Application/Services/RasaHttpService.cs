using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Assistant.Application.Interfaces;
using Assistant.Domain;
using Assistant.Domain.Configuration;
using Assistant.Domain.RasaHttpModel;
using Assistant.Domain.ViewModels;
using Assistant.Domain.ViewModels.Admin;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Assistant.Application.Services
{
    public class RasaHttpService : IRasaHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationConfiguration _applicationConfiguration;
        private readonly ILogger<RasaHttpService> _logger;

        public RasaHttpService(
            HttpClient httpClient,
            IOptionsSnapshot<ApplicationConfiguration> applciationConfiguration,
            ILogger<RasaHttpService> logger
        )
        {
            _httpClient = httpClient;
            _applicationConfiguration = applciationConfiguration.Value;
            _logger = logger;
        }

        public async Task<BotResponseViewModel> PostMessageToBotAsync(BotMessageViewModel botMessage, CancellationToken cancellationToken)
        {
            var content = new StringContent(JsonSerializer.Serialize(new BotMessage { Message = botMessage.Message, Sender = botMessage.Sender }));
            var result = await _httpClient.PostAsync($"{_applicationConfiguration.RasaEndpointAddress}/webhooks/rest/webhook", content, cancellationToken);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsStringAsync();
                var botResponse = JsonSerializer.Deserialize<List<BotResponse>>(response).FirstOrDefault(); // TODO: not safe code, TODO: explore: why it's a list?

                if (botResponse == null)
                    _logger.LogWarning("RASA hasn't responded anything");

                return new BotResponseViewModel
                {
                    // Recipient = botResponse.Recipient,
                    Message = botResponse != null ? botResponse.Message : "[EMPTY MESSAGE]",
                };
            }

            throw new Exception($"There were an issue calling the bot at: '{_applicationConfiguration.RasaEndpointAddress}/webhooks/rest/webhook'");
        }

        public async Task<HttpResponseMessage> GetHealthCheckAsync(CancellationToken cancellationToken)
        {
            var result = await _httpClient.GetAsync($"{_applicationConfiguration.RasaEndpointAddress}");

            return result;
        }

        public async Task<IEnumerable<Slot>> GetTrackerSlotsAsync(string userId, CancellationToken cancellationToken)
        {
            var result = await _httpClient.GetAsync($"{_applicationConfiguration.RasaEndpointAddress}/conversations/{userId}/tracker", cancellationToken);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsStringAsync();

                var tracker = JsonSerializer.Deserialize<TrackerResponse>(response);

                var parsedSlotList = ParseToSlot(tracker.Slots);

                return parsedSlotList;
            }
            else
                _logger.LogWarning($"Cannot access tracker for '{userId}'");

            return Enumerable.Empty<Slot>();
        }

        public async Task<bool> SaveSlotsAsnyc(string userId, IEnumerable<Slot> slots, CancellationToken cancellationToken)
        {
            var eventSlots = slots.Select(s =>
                new SlotEventRequest
                {
                    Name = s.Key,
                    Value = s.Value == null ? s.Values : s.Value
                }
            );

            var content = new StringContent(JsonSerializer.Serialize(eventSlots));

            var result = await _httpClient.PostAsync($"{_applicationConfiguration.RasaEndpointAddress}/conversations/{userId}/tracker/events", content, cancellationToken);

            if (!result.IsSuccessStatusCode)
                _logger.LogWarning($"Could not save user slots for '{userId}'");

            return result.IsSuccessStatusCode;
        }

        private static IList<Slot> ParseToSlot(IDictionary<string, object> slotObjects)
        {
            var slotList = new List<Slot>();

            foreach (var slotObject in slotObjects)
            {
                var slot = new Slot
                {
                    Key = slotObject.Key,
                };

                if (slotObject.Value is JsonElement)
                {
                    var jsonElement = (JsonElement)slotObject.Value; // It's an array

                    if (jsonElement.ValueKind == JsonValueKind.Array)
                        slot.Values = jsonElement.EnumerateArray().Select(i => i.GetString()).ToList();
                    else
                        slot.Value = jsonElement.GetString();
                }

                slotList.Add(slot);
            }

            return slotList;
        }
    }
}
