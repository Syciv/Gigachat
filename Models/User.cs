using System.Text.Json.Serialization;

namespace Gigachat.Models
{
    public class User
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; }
    }
}
