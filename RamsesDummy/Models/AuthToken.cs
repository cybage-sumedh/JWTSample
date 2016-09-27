using Newtonsoft.Json;

namespace RamsesDummy.Models
{
    public class AuthToken
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AuthTokenKey { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string  TokenType { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }
    }
}