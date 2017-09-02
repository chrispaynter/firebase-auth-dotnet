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
    public class FirebaseAuthService : IFirebaseAuthService, IDisposable
    {
        private FirebaseAuthOptions options;
        private readonly HttpClient client;
        private string identityTookkitUrl = "https://www.googleapis.com/identitytoolkit/v3/relyingparty";
        private string secureTokenUrl = "https://securetoken.googleapis.com/v1/token?key={0}";
        private static JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        /// <param name="options">Options to configure the service to communicate with Firebase REST API.</param>
        public FirebaseAuthService(FirebaseAuthOptions options)
        {
            this.options = options;
            client = new HttpClient();
            secureTokenUrl = string.Format(secureTokenUrl, options.WebApiKey);
        }

        private string IdentityToolKitUrl(string endpoint)
        {
            return $"{identityTookkitUrl}/{endpoint}?key={options.WebApiKey}";
        }

        /// <summary>
        /// Exchange a custom Auth token for an ID and refresh token
        /// </summary>
        public async Task<VerifyCustomTokenResponse> VerifyCustomTokenAsync(VerifyCustomTokenRequest request)
        {
            return await PostAsync<VerifyCustomTokenResponse>(IdentityToolKitUrl("verifyCustomToken"), request);
        }

        /// <summary>
        /// Exchanges a refresh token for a new Id token with a renewed expiry.
        /// </summary>
        public async Task<ExchangeRefreshTokenResponse> ExchangeRefreshTokenAsync(ExchangeRefreshTokenRequest request)
        {
            return await PostAsync<ExchangeRefreshTokenResponse>(secureTokenUrl, request);
        }

        /// <summary>
        /// Creates a new user in Firebase.
        /// </summary>
        public async Task<SignUpNewUserResponse> SignUpNewUserAsync(SignUpNewUserRequest request)
        {
            return await PostAsync<SignUpNewUserResponse>(IdentityToolKitUrl("signupNewUser"), request);
        }

        /// <summary>
        /// Verifies the password for a given user. This is equivalent to signing the user in
        /// with an email and password.
        /// </summary>
        public async Task<VerifyPasswordResponse> VerifyPasswordAsync(VerifyPasswordRequest request)
        {
            return await PostAsync<VerifyPasswordResponse>(IdentityToolKitUrl("verifyPassword"), request);
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

                // Let's try and construct a FirebaseAuthException from the response.

                var errorResponse = JsonConvert.DeserializeObject<FirebaseAuthErrorResponseWrapper>(responseJson, jsonSettings);
                firebaseException = new FirebaseAuthException($"Call to Firebase Auth API resulted in a bad request: {errorResponse.Error.Message}", e)
                {
                    Error = errorResponse.Error,
                    ResponseJson = responseJson
                };

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