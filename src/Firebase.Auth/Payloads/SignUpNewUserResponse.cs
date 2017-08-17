namespace Firebase.Auth.Payloads
{
    /// <summary>
    /// Response object after signing up a new user with an email and password.
    /// </summary>
    public class SignUpNewUserResponse : BaseResponse
    {
        /// <summary>
        /// A Firebase Auth ID token for the newly created user.
        /// </summary>
        public string IdToken { get; set; }

        /// <summary>
        /// The email for the newly created user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// A Firebase Auth refresh token for the newly created user.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// The number of seconds in which the ID token expires.
        /// </summary>
        public int ExpiresIn { get; set; }

        /// <summary>
        /// The uid of the newly created user.
        /// </summary>
        public string LocalId { get; set; }
    }
}