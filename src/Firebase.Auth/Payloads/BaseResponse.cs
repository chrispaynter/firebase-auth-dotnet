namespace Firebase.Auth.Payloads
{
    public class BaseResponse
    {
        /// <summary>
        /// The request type, always "identitytoolkit#SignupNewUserResponse".
        /// </summary>
        public string Kind { get; set; }
    }
}
