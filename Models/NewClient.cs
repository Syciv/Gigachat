using System.Text.Json.Serialization;

namespace Chatt.Models
{
    public class NewClient
    {
        [JsonPropertyName("clientName")]
        public string ClientName { get; set; }
    }
}
