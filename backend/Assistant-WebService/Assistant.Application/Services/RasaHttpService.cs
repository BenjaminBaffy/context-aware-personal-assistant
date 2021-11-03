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
using Microsoft.Extensions.Options;

namespace Assistant.Application.Services
{
    public class RasaHttpService : IRasaHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationConfiguration _applicationConfiguration;

        public RasaHttpService(
            HttpClient httpClient,
            IOptionsSnapshot<ApplicationConfiguration> applciationConfiguration
        )
        {
            _httpClient = httpClient;
            _applicationConfiguration = applciationConfiguration.Value;
        }

        public async Task<BotResponseViewModel> PostMessageToBot(BotMessageViewModel botMessage)
        {
            var content = new StringContent(JsonSerializer.Serialize(new BotMessage{ Message = botMessage.Message, Sender = botMessage.Sender }));
            var result = await _httpClient.PostAsync($"{_applicationConfiguration.RasaEndpointAddress}/webhooks/rest/webhook", content);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsStringAsync();
                var botResponse = JsonSerializer.Deserialize<List<BotResponse>>(response).First(); // TODO: not safe code, TODO: explore: why it's a list?

                return new BotResponseViewModel
                {
                    Recipient = botResponse.Recipient,
                    Message = botResponse.Message,
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
