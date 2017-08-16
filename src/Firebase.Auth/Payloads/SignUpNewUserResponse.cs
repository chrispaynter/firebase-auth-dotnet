namespace Firebase.Auth.Payloads
{
    public class SignUpNewUserResponse
    {
        /// <summary>
        /// The request type, always "identitytoolkit#SignupNewUserResponse".
        /// </summary>
        public string Kind { get; set; }

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