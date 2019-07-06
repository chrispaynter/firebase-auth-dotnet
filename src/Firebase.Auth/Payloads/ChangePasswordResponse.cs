using System;
using System.Collections.Generic;

namespace Firebase.Auth.Payloads
{
    /// <summary>
    /// You can change a user's password by issuing an HTTP POST request to the Auth setAccountInfo endpoint.
    /// </summary>
    public class ChangePasswordResponse : BaseRequest
    {
        /// <summary>
        /// The request type, always "identitytoolkit#SetAccountInfoResponse".
        /// </summary>
        public string kind { get; set; }

        /// <summary>
        /// The uid of the current user.
        /// </summary>
        public string localId { get; set; }

        /// <summary>
        /// User's email address.
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Hash version of password.
        /// </summary>
        public string passwordHash { get; set; }

        /// <summary>
        /// List of all linked provider objects which contain "providerId" and "federatedId".
        /// </summary>
        public IList<ProviderUserInfo> providerUserInfo { get; set; }

        /// <summary>
        /// New Firebase Auth ID token for user.
        /// </summary>
        public string idToken { get; set; }

        /// <summary>
        /// A Firebase Auth refresh token.
        /// </summary>
        public string refreshToken { get; set; }

        /// <summary>
        /// The number of seconds in which the ID token expires.
        /// </summary>
        public string expiresIn { get; set; }
    }

    public class ProviderUserInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string providerId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string federatedId { get; set; }
    }
}
