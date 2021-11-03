using System.Text.Json.Serialization;

namespace Assistant.Domain.RasaHttpModel
{
    public class BotMessage
    {
        [JsonPropertyName("sender")]
        public string Sender { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
