using System;
namespace Firebase.Auth.Payloads
{
    /// <summary>
    /// You can get a user's data by issuing an HTTP POST request to the Auth getAccountInfo endpoint.
    /// </summary>
    public class GetUserDataRequest : BaseRequest
    {
        /// <summary>
        /// The Firebase ID token of the account.
        /// </summary>
        public string IdToken { get; set; }
    }
}
