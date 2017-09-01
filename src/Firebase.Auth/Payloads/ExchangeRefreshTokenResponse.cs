using Newtonsoft.Json;

namespace Firebase.Auth.Payloads
{
    public class ExchangeRefreshTokenResponse
    {
        /// <summary>
        /// The number of seconds in which the ID token expires.
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// 	The type of the refresh token, always "Bearer".
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// A Firebase Auth refresh token for the newly created user.
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// A Firebase Auth ID token for the newly created user.
        /// </summary>
        [JsonProperty("id_token")]
        public string IdToken { get; set; }

        /// <summary>
        /// The uid corresponding to the provided ID token.
        /// </summary>
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// 	Your Firebase project ID.
        /// </summary>
        [JsonProperty("project_id")]
        public string ProjectId { get; set; }
    }
}
