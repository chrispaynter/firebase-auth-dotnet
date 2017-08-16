namespace Firebase.Auth.Payloads
{
    public class VerifyPasswordRequest : BaseRequest
    {
        /// <summary>
        /// The email the user is signing in with.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The password for the account.
        /// </summary>
        public string Password { get; set; }
    }
}
