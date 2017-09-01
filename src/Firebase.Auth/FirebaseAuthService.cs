using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth.Payloads;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Firebase.Auth
{
    /// <summary>
    /// Service for connecting and communicating with the Firebase Auth REST API
    /// </summary>
    public class FirebaseAuthService: IFirebaseAuthService, IDisposable
    {
        private FirebaseAuthOptions options;
        private readonly HttpClient client;
        private string url = "https://www.googleapis.com/identitytoolkit/v3/relyingparty";
        private static JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        /// <param name="options">Options to configure the service to communicate with Firebase REST API.</param>
        public FirebaseAuthService(FirebaseAuthOptions options)
        {
            this.options = options;
            this.client = new HttpClient();
        }

        private string Url(string endpoint)
        {
            return $"{url}/{endpoint}?key={options.WebApiKey}";
        }

        /// <summary>
        /// Creates a new user in Firebase.
        /// </summary>
        public async Task<SignUpNewUserResponse> SignUpNewUserAsync(SignUpNewUserRequest request)
        {
            return await PostAsync<SignUpNewUserResponse>(Url("signupNewUser"), request);
        }

        /// <summary>
        /// Verifies the password for a given user. This is equivalent to signing the user in
        /// with an email and password.
        /// </summary>
        public async Task<VerifyPasswordResponse> VerifyPasswordAsync(VerifyPasswordRequest request)
        {
            return await PostAsync<VerifyPasswordResponse>(Url("verifyPassword"), request);
        }

        private async Task<TResponse> PostAsync<TResponse>(string endpoint, object request) where TResponse : class
        {
            string responseJson = "";

            try
            {
                var content = JsonConvert.SerializeObject(request, jsonSettings);
                var payload = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await this.client.PostAsync(endpoint, payload);
                responseJson = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<TResponse>(responseJson);
            }
            catch (Exception e)
            {
                FirebaseAuthException firebaseException;

                try
                {
                    // Let's try and construct a FirebaseAuthException from the response.

                    var errorResponse = JsonConvert.DeserializeObject<FirebaseAuthErrorResponseWrapper>(responseJson, jsonSettings);
                    firebaseException = new FirebaseAuthException($"Call to Firebase Auth API resulted in a bad request: {errorResponse.Error.Message}", e)
                    {
                        Error = errorResponse.Error,
                        ResponseJson = responseJson
                    };
                }
                catch (Exception ex)
                {
                    // If we can't, literally nothing we can do but return a generic exception.

                    throw new FirebaseAuthException("An unexpected exception occured while trying to deserialize the error response from Firebase. This probably means that the exception is not from Firebase, but from the HttpClient request.", ex)
                    {
                        OriginRequestException = e,
                        ResponseJson = responseJson
                    };
                }

                throw firebaseException;
            }
        }

        /// <summary>
        /// Cleans up the web client after usage.
        /// </summary>
        public void Dispose()
        {
            client.Dispose();
        }
    }
}