using System.Text.Json.Serialization;

namespace Chatt.Models
{
    public class UserAuthentification
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("passwordHash")]
        public byte[] PasswordHash { get; set; }
    }
}
