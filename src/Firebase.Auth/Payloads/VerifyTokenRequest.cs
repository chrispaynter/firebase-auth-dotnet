namespace Firebase.Auth.Payloads
{
    public class VerifyRefreshTokenRequest
    {
        public string GrantType { get; } = "refresh_token";

        /// <summary>
        /// The refresh token the user received when last signing in.
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
