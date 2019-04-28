namespace Firebase.Auth.Payloads
{
    public class VerifyRefreshTokenResponse : BaseResponse
    {
        /// <summary>
        /// A Firebase Auth ID token for the user.
        /// </summary>
        public string IdToken { get; set; }

        /// <summary>
        /// A Firebase Auth refresh token for the user.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// The number of seconds in which the ID token expires.
        /// </summary>
        public int ExpiresIn { get; set; }

        /// <summary>
        /// The uid corresponding to the provided ID token.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The Firebase project ID.
        /// </summary>
        public string ProjectId { get; set; }
    }
}