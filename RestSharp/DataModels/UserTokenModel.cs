using Newtonsoft.Json;

namespace RestSharpTest.DataModels
{
    public class UserTokenModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
