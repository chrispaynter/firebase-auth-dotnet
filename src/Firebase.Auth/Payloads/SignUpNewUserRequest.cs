namespace Firebase.Auth.Payloads
{
    namespace Firebase.Auth.Payloads
    {
        public class SignUpNewUserRequest : BaseRequest
        {
            /// <summary>
            /// 	The email for the user to create.
            /// </summary>
            public string Email { get; set; }

            /// <summary>
            /// The password for the user to create.
            /// </summary>
            public string Password { get; set; }
        }
    }
}