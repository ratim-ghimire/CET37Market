namespace CET37Market.Common.Models
{
    using Newtonsoft.Json;
    //Class responsible for Token request
    public class TokenResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }


        [JsonProperty("expiration")]
        public string Expiration { get; set; }
    }
}
