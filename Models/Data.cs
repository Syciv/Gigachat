using System.Text.Json.Serialization;

namespace Chatt.Models
{
    public class Data
    {
        [JsonPropertyName("message")]
        public Message Message { get; set; }

        [JsonPropertyName("newClient")]
        public NewClient NewClient { get; set; }

        [JsonPropertyName("userAuthentification")]
        public UserAuthentification UserAuthentification { get; set; }

        [JsonPropertyName("userRegistration")]
        public UserRegistration UserRegistration { get; set; }
    }
}
