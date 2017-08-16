namespace Firebase.Auth.Payloads
{
    public class SignUpNewUserResponse
    {
        public string Kind { get; set; }
        public string IdToken { get; set; }
        public string Email { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
        public string LocalId { get; set; }
    }
}