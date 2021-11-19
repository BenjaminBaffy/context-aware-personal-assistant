using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Assistant.Application.Interfaces;
using Assistant.Domain.Configuration;
using Assistant.Domain.RasaHttpModel;
using Assistant.Domain.ViewModels;
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

        public async Task<BotResponseViewModel> PostMessageToBot(BotMessageViewModel botMessage)
        {
            var content = new StringContent(JsonSerializer.Serialize(new BotMessage{ Message = botMessage.Message, Sender = botMessage.Sender }));
            var result = await _httpClient.PostAsync($"{_applicationConfiguration.RasaEndpointAddress}/webhooks/rest/webhook", content);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsStringAsync();
                var botResponse = JsonSerializer.Deserialize<List<BotResponse>>(response).FirstOrDefault(); // TODO: not safe code, TODO: explore: why it's a list?

                if (botResponse == null)
                    _logger.LogWarning("RASA hasn't responded anything");

                return new BotResponseViewModel
                {
                    Recipient = botResponse.Recipient,
                    Message = botResponse == null ? botResponse.Message : "[EMPTY MESSAGE]",
                };
            }

            throw new Exception($"There were an issue calling the bot at: '{_applicationConfiguration.RasaEndpointAddress}/webhooks/rest/webhook'");
        }

        public async Task<HttpResponseMessage> GetHealthCheck()
        {
            var result = await _httpClient.GetAsync($"{_applicationConfiguration.RasaEndpointAddress}");

            return result;
        }
    }
}
