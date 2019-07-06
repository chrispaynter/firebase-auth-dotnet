namespace Firebase.Auth.Payloads
{
    public class SendVerificationEmailResponse : BaseResponse
    {
        /// <summary>
        /// The request type, always "identitytoolkit#GetOobConfirmationCodeResponse".
        /// </summary>
        public string Kind { get; set; }

        /// <summary>
        /// The email of the account.
        /// </summary>
        public string Email { get; set; }
    }
}
