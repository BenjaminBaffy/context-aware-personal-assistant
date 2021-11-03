using System.Text.Json.Serialization;

namespace Assistant.Domain.RasaHttpModel
{
    public class BotResponse
    {
        [JsonPropertyName("recipient_id")]
        public string Recipient { get; set; }

        [JsonPropertyName("text")]
        public string Message { get; set; }
    }
}
