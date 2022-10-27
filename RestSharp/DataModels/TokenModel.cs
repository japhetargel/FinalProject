using Newtonsoft.Json;

namespace RestSharpTest.DataModels
{
    public class TokenModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
