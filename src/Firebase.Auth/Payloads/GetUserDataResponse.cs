using System;
using System.Collections.Generic;

namespace Firebase.Auth.Payloads
{
    public class GetUserDataResponse : BaseResponse
    {
        /// <summary>
        /// The request type, always "identitytoolkit#GetAccountInfoResponse".
        /// </summary>
        public string kind { get; set; }

        /// <summary>
        /// The account associated with the given Firebase ID token. Check below for more details.
        /// </summary>
        public IList<User> users { get; set; }
    }

    public class ProviderUserData
    {
        public string providerId { get; set; }
        public string displayName { get; set; }
        public string photoUrl { get; set; }
        public string federatedId { get; set; }
        public string email { get; set; }
        public string rawId { get; set; }
        public string screenName { get; set; }
    }

    public class User
    {
        /// <summary>
        /// The uid of the current user.
        /// </summary>
        public string localId { get; set; }

        /// <summary>
        /// The email of the account.
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Whether or not the account's email has been verified.
        /// </summary>
        public bool emailVerified { get; set; }

        /// <summary>
        /// The display name for the account.
        /// </summary>
        public string displayName { get; set; }

        /// <summary>
        /// List of all linked provider objects which contain "providerId" and "federatedId".
        /// </summary>
        public IList<ProviderUserData> providerUserInfo { get; set; }

        /// <summary>
        /// The photo Url for the account.
        /// </summary>
        public string photoUrl { get; set; }

        /// <summary>
        /// Hash version of password.
        /// </summary>
        public string passwordHash { get; set; }

        /// <summary>
        /// The timestamp, in milliseconds, that the account password was last changed.
        /// </summary>
        public double passwordUpdatedAt { get; set; }

        /// <summary>
        /// The timestamp, in seconds, which marks a boundary, before which Firebase ID token are considered revoked.
        /// </summary>
        public string validSince { get; set; }

        /// <summary>
        /// Whether the account is disabled or not.
        /// </summary>
        public bool disabled { get; set; }

        /// <summary>
        /// The timestamp, in milliseconds, that the account last logged in at.
        /// </summary>
        public string lastLoginAt { get; set; }

        /// <summary>
        /// The timestamp, in milliseconds, that the account was created at.
        /// </summary>
        public string createdAt { get; set; }

        /// <summary>
        /// Whether the account is authenticated by the developer.
        /// </summary>
        public bool customAuth { get; set; }
    }
}
