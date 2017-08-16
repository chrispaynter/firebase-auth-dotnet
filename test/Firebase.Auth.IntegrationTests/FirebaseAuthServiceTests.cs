using Firebase.Auth.Payloads;
using Firebase.Auth.Payloads.Firebase.Auth.Payloads;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Firebase.Auth.Tests
{
    public class FirebaseAuthServiceTests
    {
        private readonly string webApiKey;
        private readonly string knownValidEmail;
        private readonly string knownValidPassword;

        public FirebaseAuthServiceTests()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("secrets.json")
                .Build();

            webApiKey = config["firebaseWebApiKey"];
            knownValidEmail = config["validUserEmail"];
            knownValidPassword = config["validUserPassword"];
        }

        #region SignUpNewUser

        private async Task<SignUpNewUserResponse> SignUpNewUser_ValidCredentials()
        {
            using (var service = CreateService())
            {
                var request = new SignUpNewUserRequest()
                {
                    Email = GenerateValidEmail(),
                    Password = "testasdf32t23t23t1234"
                };

                return await service.SignUpNewUser(request);
            }
        }

        [Fact]
        public async Task SignUpNewUser_ValidCredentials_ReturnsIdToken()
        {
            var response = await SignUpNewUser_ValidCredentials();
            Assert.NotNull(response.IdToken);
            Assert.NotEmpty(response.IdToken);
        }
        [Fact]
        public async Task SignUpNewUser_ValidCredentials_ReturnsEmail()
        {
            var response = await SignUpNewUser_ValidCredentials();
            Assert.NotNull(response.Email);
            Assert.NotEmpty(response.Email);
        }

        [Fact]
        public async Task SignUpNewUser_ValidCredentials_ReturnsRefreshToken()
        {
            var response = await SignUpNewUser_ValidCredentials();
            Assert.NotNull(response.RefreshToken);
            Assert.NotEmpty(response.RefreshToken);
        }

        [Fact]
        public async Task SignUpNewUser_ValidCredentials_ReturnsExpiresIn()
        {
            var response = await SignUpNewUser_ValidCredentials();
            Assert.True(response.ExpiresIn > 0);
        }

        [Fact]
        public async Task SignUpNewUser_ValidCredentials_ReturnsLocalId()
        {
            var response = await SignUpNewUser_ValidCredentials();
            Assert.NotNull(response.LocalId);
            Assert.NotEmpty(response.LocalId);
        }

        [Fact]
        public async Task SignUpNewUser_ExistingEmail_ThrowsEmailExists()
        {
            using (var service = CreateService())
            {
                var request = new SignUpNewUserRequest()
                {
                    Email = knownValidEmail,
                    Password = "anyoldpassword"
                };

                var exception = await Assert.ThrowsAsync<FirebaseAuthException>(async () => await service.SignUpNewUser(request));
                Assert.Equal(FirebaseAuthMessageType.EmailExists, exception.Error?.MessageType);
            }
        }

        [Fact]
        public async Task SignUpNewUser_WeakPassword_ThrowsWeakPassword()
        {
            using (var service = CreateService())
            {
                var request = new SignUpNewUserRequest()
                {
                    Email = GenerateValidEmail(),
                    Password = "12345"
                };

                var exception = await Assert.ThrowsAsync<FirebaseAuthException>(async () => await service.SignUpNewUser(request));
                Assert.Equal(FirebaseAuthMessageType.WeakPassword, exception.Error?.MessageType);
            }
        }

        [Fact]
        public async Task SignUpNewUser_InvalidEmail_ThrowsInvalidEmail()
        {
            using (var service = CreateService())
            {
                var request = new SignUpNewUserRequest()
                {
                    Email = "invalidemail",
                    Password = "validpassword"
                };

                var exception = await Assert.ThrowsAsync<FirebaseAuthException>(async () => await service.SignUpNewUser(request));
                Assert.Equal(FirebaseAuthMessageType.InvalidEmail, exception.Error?.MessageType);
            }
        }

        [Fact]
        public async Task SignUpNewUser_NoEmail_ThrowsMissingPassword()
        {
            using (var service = CreateService())
            {
                var request = new SignUpNewUserRequest()
                {
                    Email = "valid@valid.com",
                    Password = ""
                };

                var exception = await Assert.ThrowsAsync<FirebaseAuthException>(async () => await service.SignUpNewUser(request));
                Assert.Equal(FirebaseAuthMessageType.MissingPassword, exception.Error?.MessageType);
            }
        }

        [Fact]
        public async Task SignUpNewUser_NoEmail_ThrowsMissingEmail()
        {
            using (var service = CreateService())
            {
                var request = new SignUpNewUserRequest()
                {
                    Email = "",
                    Password = "asdfasdfasdfasdf"
                };

                var exception = await Assert.ThrowsAsync<FirebaseAuthException>(async () => await service.SignUpNewUser(request));
                Assert.Equal(FirebaseAuthMessageType.MissingEmail, exception.Error?.MessageType);
            }
        }

        #endregion

        #region Helpers

        public static string GenerateValidEmail()
        {
            return $"{DateTime.UtcNow.Ticks}@validdomain.com";
        }

        public FirebaseAuthService CreateService()
        {
            return new FirebaseAuthService(new FirebaseAuthOptions(webApiKey));
        }

        #endregion
    }
}