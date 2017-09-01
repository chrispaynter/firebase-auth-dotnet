using Newtonsoft.Json;

namespace Firebase.Auth.Payloads
{
    public class ExchangeRefreshTokenRequest
    {
        /// <summary>
        /// The refresh token's grant type, always "refresh_token".
        /// </summary>
        [JsonProperty("grant_type")]
        public string GrantType { get { return "refresh_token"; } }

        /// <summary>
        /// A Firebase Auth refresh token.
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
