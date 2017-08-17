namespace Firebase.Auth
{
    /// <summary>
    /// Configuration options for connecting and authenticating with the Firebase Auth REST API
    /// </summary>
    public class FirebaseAuthOptions
    {
        /// <summary>
        /// </summary>
        /// <param name="webApiKey">Your project's web API key</param>
        public FirebaseAuthOptions(string webApiKey)
        {
            if (string.IsNullOrEmpty(webApiKey))
            {
                throw new System.ArgumentException("message", nameof(webApiKey));
            }

            WebApiKey = webApiKey;
        }

        /// <summary>
        /// Your project's API key, found in the project settings section
        /// of the Firebase Console
        /// </summary>
        public string WebApiKey { get; set; }
    }
}