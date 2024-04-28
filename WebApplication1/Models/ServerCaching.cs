// ServerCaching.cs

using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class ServerCaching
    {
        [JsonPropertyName("ipServer")]
        public string ServerIp {get; set;}

        [JsonPropertyName("nbPlayer")]
        public int PlayerNumber {get; set;}

        [JsonPropertyName("avgRank")]
        public string AverageRank {get; set;}
    }
}
