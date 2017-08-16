namespace Firebase.Auth
{
    public class FirebaseAuthOptions
    {
        public FirebaseAuthOptions(string webApiKey)
        {
            if (string.IsNullOrEmpty(webApiKey))
            {
                throw new System.ArgumentException("message", nameof(webApiKey));
            }

            WebApiKey = webApiKey;
        }

        public string WebApiKey { get; set; }
    }
}