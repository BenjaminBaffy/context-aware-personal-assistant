using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Assistant.Domain.RasaHttpModel
{
    public class TrackerResponse
    {
        [JsonPropertyName("slots")]
        public IDictionary<string, object> Slots { get; set; } // TODO: think of better model?!
    }
}
