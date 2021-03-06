using System.Text.Json.Serialization;

namespace Gigachat.Models
{
    public class Message
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("time")]
        public string Time { get; set; }
    }
}
