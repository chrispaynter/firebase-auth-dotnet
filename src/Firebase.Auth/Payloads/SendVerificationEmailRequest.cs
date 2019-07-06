using System;
namespace Firebase.Auth.Payloads
{
    public class SendVerificationEmailRequest : BaseRequest
    {
        /// <summary>
        /// The type of confirmation code to send. Should always be "VERIFY_EMAIL".
        /// </summary>
        public string RequestType { get; set; }

        /// <summary>
        /// The Firebase ID token of the user to verify.
        /// </summary>
        public string IdToken { get; set; }
    }
}
