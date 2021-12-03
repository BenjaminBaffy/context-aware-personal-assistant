using System.Text.Json.Serialization;

namespace Assistant.Domain.RasaHttpModel
{
    public class SlotEventRequest : EventRequest
    {
        [JsonPropertyName("event")]
        public new string Event => "slot";
    }
}
