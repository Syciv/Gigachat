using System.Text.Json.Serialization;

namespace Gigachat.Models
{
    public class UserAuthentification
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
