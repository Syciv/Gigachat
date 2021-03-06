using System.Text.Json.Serialization;

namespace Gigachat.Models
{
    public class UserRegistration
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("surname")]
        public string Surname { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
