namespace Firebase.Auth.Payloads
{
    /// <summary>
    /// Request object for signing up a new user with an email and password.
    /// </summary>
    public class SignUpNewUserRequest : BaseRequest
    {
        /// <summary>
        /// The email for the user to create.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The password for the user to create.
        /// </summary>
        public string Password { get; set; }
    }
}