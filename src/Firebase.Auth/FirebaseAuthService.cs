using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth.Payloads;
using Firebase.Auth.Payloads.Firebase.Auth.Payloads;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Firebase.Auth
{
    public class FirebaseAuthService: IDisposable
    {
        private FirebaseAuthOptions options;
        private readonly HttpClient client;
        private string url = "https://www.googleapis.com/identitytoolkit/v3/relyingparty";
        private static JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        public FirebaseAuthService(FirebaseAuthOptions options)
        {
            this.options = options;
            this.client = new HttpClient();
        }

        private string Url(string endpoint)
        {
            return $"{url}/{endpoint}?key={options.WebApiKey}";
        }

        public async Task<SignUpNewUserResponse> SignUpNewUser(SignUpNewUserRequest request)
        {
            return await Post<SignUpNewUserResponse>(Url("signupNewUser"), request);
        }

        public async Task<VerifyPasswordResponse> VerifyPassword(VerifyPasswordRequest request)
        {
            return await Post<VerifyPasswordResponse>(Url("verifyPassword"), request);
        }

        private async Task<TResponse> Post<TResponse>(string endpoint, object request) where TResponse : class
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
                try
                {
                    var errorResponse = JsonConvert.DeserializeObject<FirebaseAuthErrorResponseWrapper>(responseJson, jsonSettings);
                    throw new FirebaseAuthException($"Call to Firebase Auth API resulted in a bad request: {errorResponse.Error.Message}", e)
                    {
                        Error = errorResponse.Error,
                        ResponseJson = responseJson
                    };
                }
                catch (JsonSerializationException ex)
                {
                    throw new FirebaseAuthException("Deserializing Firebase Auth API response failed", ex)
                    {
                        OriginRequestException = e,
                        ResponseJson = responseJson
                    };
                }


            }
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}