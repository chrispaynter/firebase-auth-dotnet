namespace Firebase.Auth.Payloads
{
    namespace Firebase.Auth.Payloads
    {
        public class SignUpNewUserRequest
        {
            /// <summary>
            /// 	The email for the user to create.
            /// </summary>
            public string Email { get; set; }

            /// <summary>
            /// The password for the user to create.
            /// </summary>
            public string Password { get; set; }

            /// <summary>
            /// Whether or not to return an ID and refresh token. Should always be true.
            /// </summary>
            public bool ReturnSecureToken { get; set; } = true;
        }
    }
}