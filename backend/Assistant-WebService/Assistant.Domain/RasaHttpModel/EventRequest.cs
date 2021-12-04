using System;
using System.Text.Json.Serialization;

namespace Assistant.Domain.RasaHttpModel
{
    public class EventRequest
    {
        [JsonPropertyName("event")]
        public string Event { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("value")]
        public object Value { get; set; } // NOTE: don't do this on a real project
        [JsonPropertyName("timestamp")]
        public DateTimeOffset? Timestamp { get; set; }
    }
}
