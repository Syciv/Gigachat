using System.Text.Json.Serialization;

namespace Chatt.Models
{
    public class Data
    {
        [JsonPropertyName("message")]
        public Message Message { get; set; }
        [JsonPropertyName("newClient")]
        public NewClient NewClient { get; set; }
    }
}
