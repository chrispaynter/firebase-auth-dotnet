using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth.Payloads;
using Firebase.Auth.Payloads.Firebase.Auth.Payloads;
using Newtonsoft.Json;

namespace Firebase.Auth
{
    public class FirebaseAuthService
    {
        private FirebaseAuthOptions options;
        private readonly HttpClient client;
        private string url = "https://www.googleapis.com/identitytoolkit/v3/relyingparty";

        public FirebaseAuthService(FirebaseAuthOptions options)
        {
            url = $"[endpoint]?key{options.FirebaseKey}";
            this.options = options;
            this.client = new HttpClient();
        }

        private string Url(string endpoint)
        {
            return $"{url}/{endpoint}?key={options.FirebaseKey}";
        }

        public async Task<SignUpNewUserResponse> SignUpNewUser(SignUpNewUserRequest request)
        {
            return await Post<SignUpNewUserResponse>(Url("signupNewUser"), request);
        }

        private async Task<TResponse> Post<TResponse>(string endpoint, object request)
        {
            try
            {
                var content = JsonConvert.SerializeObject(request);
                var response = await this.client.PostAsync(endpoint, new StringContent(content, Encoding.UTF8, "application/json"));
                var responseData = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                AuthErrorReason errorReason = GetFailureReason(responseData);
                throw new FirebaseAuthException(googleUrl, postContent, responseData, ex, errorReason);
            }
        }
    }
}