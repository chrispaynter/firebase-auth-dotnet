using Firebase.Auth.Payloads.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firebase.Auth.Payloads
{
    public class GetAccountInfoResponse
    {
        /// <summary>
        /// The request type, always "identitytoolkit#GetAccountInfoResponse".
        /// </summary>
        public string Kind { get; set; }

        /// <summary>
        /// The account associated with the given Firebase ID token. Check below for more details.
        /// </summary>
        public IEnumerable<FirebaseUser> Users { get; set; }
    }
}
