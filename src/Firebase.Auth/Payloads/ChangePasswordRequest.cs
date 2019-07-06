using System;
namespace Firebase.Auth.Payloads
{
    /// <summary>
    /// You can change a user's password by issuing an HTTP POST request to the Auth setAccountInfo endpoint.
    /// </summary>
    public class ChangePasswordRequest : BaseRequest
    {
        /// <summary>
        /// A Firebase Auth ID token for the user.
        /// </summary>
        public string IdToken { get; set; }

        /// <summary>
        /// User's new password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Whether or not to return an ID and refresh token.
        /// </summary>
        public string ReturnSecureToken { get; set; }
    }
}
