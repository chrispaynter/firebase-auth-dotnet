namespace Firebase.Auth.Payloads
{
    public class VerifyCustomTokenRequest : BaseRequest
    {
        /// <summary>
        /// A Firebase Auth custom token from which to create an ID and refresh token pair.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Whether or not to return an ID and refresh token. Should always be true.
        /// </summary>
        public bool ResturnSecureToken { get { return true; } }
    }
}
