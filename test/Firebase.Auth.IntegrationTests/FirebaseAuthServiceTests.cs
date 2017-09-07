using Firebase.Auth.Payloads;
using Firebase.Auth.Tokens;
using Firebase.Test;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Firebase.Auth.Tests
{
    public class FirebaseAuthServiceTests
    {
        private readonly string webApiKey;
        private readonly string knownValidEmail;
        private readonly string knownValidPassword;
        private readonly string knownDisabledEmail;
        private readonly string knownDisabledPassword;
        private readonly string knownRefreshToken;

        private readonly string adminPrivateKey;
        private readonly string adminClientEmail;


        public FirebaseAuthServiceTests()
        {
            var section = SharedTestConfiguration.Configuration.GetSection("projectContext");

            webApiKey = section["firebaseWebApiKey"];
            knownValidEmail = section["knownValidEmail"];
            knownValidPassword = section["knownVaidPassword"];
            knownDisabledEmail = section["knownDisabledEmail"];
            knownDisabledPassword = section["knownDisabledPassword"];
            knownRefreshToken = section["knownRefreshToken"];

            var adminContext = SharedTestConfiguration.Configuration.GetSection("adminContext");
            adminPrivateKey = adminContext["private_key"];
            adminClientEmail = adminContext["client_email"];
        }

        #region VerifyCustomToken

        private string MakeCustomToken(string userId)
        {
            return MakeCustomToken(userId, null);
        }
        private string MakeCustomToken(string userId, Dictionary<string, object> claims)
        {
            var tokenGen = new FirebaseTokenGenerator(adminPrivateKey, adminClientEmail);
            return tokenGen.EncodeToken(userId, claims);
        }

        private async Task<VerifyCustomTokenResponse> VerifyCustomToken_ValidRefreshToken(string token)
        {
            using (var service = CreateService())
            {
                var request = new VerifyCustomTokenRequest
                {
                    Token = token
                };

                return await service.VerifyCustomTokenAsync(request);
            }
        }


        // TODO: It would be nice to test the situation where the private key is invalid, however I don't have time
        // to generate the PKCS7 keys right now.

        //[Fact]
        //public async Task VerifyCustomToken_InvalidPrivateKey_ThrowsInvalidCustomToken()
        //{
        //    var key = "";
        //    using (var reader = new StreamReader("invalid-private-key.key"))
        //    {
        //        key = reader.ReadToEnd();
        //    }

        //    var tokenGen = new FirebaseTokenGenerator(key, adminClientEmail);
        //    var token = tokenGen.EncodeToken("testId");

        //    using (var service = CreateService())
        //    {
        //        var request = new VerifyCustomTokenRequest()
        //        {
        //            Token = token
        //        };

        //        var exception = await Assert.ThrowsAsync<FirebaseAuthException>(async () => await service.VerifyCustomTokenAsync(request));
        //        Assert.Equal(FirebaseAuthMessageType.InvalidCustomToken, exception.Error?.MessageType);
        //    }
        //}


        #endregion

        #region ExchangeRefreshToken

        private async Task<ExchangeRefreshTokenResponse> ExchangeRefreshToken_ValidRefreshToken()
        {
            using (var service = CreateService())
            {
                var request = new ExchangeRefreshTokenRequest()
                {
                    RefreshToken = knownRefreshToken
                };

                return await service.ExchangeRefreshTokenAsync(request);
            }
        }

        [Fact]
        public async Task ExchangeRefreshToken_ValidRefreshToken_ReturnsIdToken()
        {
            //NOTE: If the known refresh token starts failing these tests, it's an indicator
            // that Google Firebase have expired it. No clear information about when these
            // refresh tokens expire, but the date right now is 2/09/2017, will be an
            // interesting test to see how long it can last for.
            var response = await ExchangeRefreshToken_ValidRefreshToken();
            Assert.NotNull(response.IdToken);
            Assert.NotEmpty(response.IdToken);
        }

        [Fact]
        public async Task ExchangeRefreshToken_ValidRefreshToken_ReturnsExpiresIn()
        {
            var response = await ExchangeRefreshToken_ValidRefreshToken();
            Assert.True(response.ExpiresIn > 0, "Expires in not set");
        }

        [Fact]
        public async Task ExchangeRefreshToken_ValidRefreshToken_ReturnsProjectId()
        {
            var response = await ExchangeRefreshToken_ValidRefreshToken();
            Assert.NotNull(response.ProjectId);
            Assert.NotEmpty(response.ProjectId);
        }

        [Fact]
        public async Task ExchangeRefreshToken_ValidRefreshToken_ReturnsRefreshToken()
        {
            var response = await ExchangeRefreshToken_ValidRefreshToken();
            Assert.NotNull(response.RefreshToken);
            Assert.NotEmpty(response.RefreshToken);
        }

        [Fact]
        public async Task ExchangeRefreshToken_ValidRefreshToken_ReturnsUserId()
        {
            var response = await ExchangeRefreshToken_ValidRefreshToken();
            Assert.NotNull(response.UserId);
            Assert.NotEmpty(response.UserId);
        }

        [Fact]
        public async Task ExchangeRefreshToken_ValidRefreshToken_TokenTypeIsBearer()
        {
            var response = await ExchangeRefreshToken_ValidRefreshToken();
            Assert.Equal("Bearer", response.TokenType);
        }

        [Fact]
        public async Task ExchangeRefreshToken_InvalidRefreshToken_ThrowsInvalidRefreshToken()
        {
            using (var service = CreateService())
            {
                var request = new ExchangeRefreshTokenRequest()
                {
                    RefreshToken = "invalidtoken"
                };

                var exception = await Assert.ThrowsAsync<FirebaseAuthException>(async () => await service.ExchangeRefreshTokenAsync(request));
                Assert.Equal(FirebaseAuthMessageType.InvalidRefreshToken, exception.Error?.MessageType);
            }
        }

        #endregion

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

                return await service.SignUpNewUserAsync(request);
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

                var exception = await Assert.ThrowsAsync<FirebaseAuthException>(async () => await service.SignUpNewUserAsync(request));
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

                var exception = await Assert.ThrowsAsync<FirebaseAuthException>(async () => await service.SignUpNewUserAsync(request));
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

                var exception = await Assert.ThrowsAsync<FirebaseAuthException>(async () => await service.SignUpNewUserAsync(request));
                Assert.Equal(FirebaseAuthMessageType.InvalidEmail, exception.Error?.MessageType);
            }
        }

        [Fact]
        public async Task SignUpNewUser_NoPassword_ThrowsMissingPassword()
        {
            using (var service = CreateService())
            {
                var request = new SignUpNewUserRequest()
                {
                    Email = "valid@valid.com",
                    Password = ""
                };

                var exception = await Assert.ThrowsAsync<FirebaseAuthException>(async () => await service.SignUpNewUserAsync(request));
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

                var exception = await Assert.ThrowsAsync<FirebaseAuthException>(async () => await service.SignUpNewUserAsync(request));
                Assert.Equal(FirebaseAuthMessageType.MissingEmail, exception.Error?.MessageType);
            }
        }

        #endregion

        #region VerifyPassword

        private async Task<VerifyPasswordResponse> VerifyPassword_ValidCredentials()
        {
            using (var service = CreateService())
            {
                var request = new VerifyPasswordRequest()
                {
                    Email = knownValidEmail,
                    Password = knownValidPassword
                };

                return await service.VerifyPasswordAsync(request);
            }
        }

        [Fact]
        public async Task VerifyPassword_ValidCredentials_ReturnsIdToken()
        {
            var response = await VerifyPassword_ValidCredentials();
            Assert.NotNull(response.IdToken);
            Assert.NotEmpty(response.IdToken);
        }

        [Fact]
        public async Task VerifyPassword_ValidCredentials_ReturnsEmail()
        {
            var response = await VerifyPassword_ValidCredentials();
            Assert.NotNull(response.Email);
            Assert.NotEmpty(response.Email);
        }

        [Fact]
        public async Task VerifyPassword_ValidCredentials_ReturnsRefreshToken()
        {
            var response = await VerifyPassword_ValidCredentials();
            Assert.NotNull(response.RefreshToken);
            Assert.NotEmpty(response.RefreshToken);
        }

        [Fact]
        public async Task VerifyPassword_ValidCredentials_ReturnsExpiresIn()
        {
            var response = await VerifyPassword_ValidCredentials();
            Assert.True(response.ExpiresIn > 0);
        }

        [Fact]
        public async Task VerifyPassword_ValidCredentials_ReturnsLocalId()
        {
            var response = await VerifyPassword_ValidCredentials();
            Assert.NotNull(response.LocalId);
            Assert.NotEmpty(response.LocalId);
        }

        [Fact]
        public async Task VerifyPassword_ValidCredentials_ReturnsRegisteredTrue()
        {
            var response = await VerifyPassword_ValidCredentials();
            Assert.True(response.Registered);
        }

        [Fact]
        public async Task VerifyPassword_RandomEmail_ThrowsEmailNotFound()
        {
            using (var service = CreateService())
            {
                var request = new VerifyPasswordRequest()
                {
                    Email = GenerateValidEmail(),
                    Password = "anyoldpassword"
                };

                var exception = await Assert.ThrowsAsync<FirebaseAuthException>(async () => await service.VerifyPasswordAsync(request));
                Assert.Equal(FirebaseAuthMessageType.EmailNotFound, exception.Error?.MessageType);
            }
        }

        [Fact]
        public async Task VerifyPassword_WrongPassword_ThrowsInvalidPassword()
        {
            using (var service = CreateService())
            {
                var request = new VerifyPasswordRequest()
                {
                    Email = knownValidEmail,
                    Password = "1234588272727272918*(*D"
                };

                var exception = await Assert.ThrowsAsync<FirebaseAuthException>(async () => await service.VerifyPasswordAsync(request));
                Assert.Equal(FirebaseAuthMessageType.InvalidPassword, exception.Error?.MessageType);
            }
        }

        [Fact]
        public async Task VerifyPassword_InvalidEmail_ThrowsInvalidEmail()
        {
            using (var service = CreateService())
            {
                var request = new VerifyPasswordRequest()
                {
                    Email = "invalidemail",
                    Password = "validpassword"
                };

                var exception = await Assert.ThrowsAsync<FirebaseAuthException>(async () => await service.VerifyPasswordAsync(request));
                Assert.Equal(FirebaseAuthMessageType.InvalidEmail, exception.Error?.MessageType);
            }
        }

        [Fact]
        public async Task VerifyPassword_NoPassword_ThrowsMissingPassword()
        {
            using (var service = CreateService())
            {
                var request = new VerifyPasswordRequest()
                {
                    Email = "valid@valid.com",
                    Password = ""
                };

                var exception = await Assert.ThrowsAsync<FirebaseAuthException>(async () => await service.VerifyPasswordAsync(request));
                Assert.Equal(FirebaseAuthMessageType.MissingPassword, exception.Error?.MessageType);
            }
        }

        [Fact]
        public async Task VerifyPassword_NoEmail_ThrowsInvalidEmail()
        {
            using (var service = CreateService())
            {
                var request = new VerifyPasswordRequest()
                {
                    Email = "",
                    Password = "asdfasdfasdfasdf"
                };

                var exception = await Assert.ThrowsAsync<FirebaseAuthException>(async () => await service.VerifyPasswordAsync(request));
                Assert.Equal(FirebaseAuthMessageType.InvalidEmail, exception.Error?.MessageType);
            }
        }

        [Fact]
        public async Task VerifyPassword_DisabledCredentials_ThrowsUserDisabled()
        {
            using (var service = CreateService())
            {
                var request = new VerifyPasswordRequest()
                {
                    Email = knownDisabledEmail,
                    Password = knownDisabledPassword
                };
                //NOTE: Make sure the user is disabled!
                var exception = await Assert.ThrowsAsync<FirebaseAuthException>(async () => await service.VerifyPasswordAsync(request));
                Assert.Equal(FirebaseAuthMessageType.UserDisabled, exception.Error?.MessageType);
            }
        }

        #endregion

        #region GetAccountInfo

        [Fact]
        public async Task GetAccountInfo_InvalidIdToken_ThrowsInvalidIdToken()
        {
            using (var service = CreateService())
            {
                var request = new GetAccountInfoRequest()
                {
                    IdToken = "invalid"
                };

                var exception = await Assert.ThrowsAsync<FirebaseAuthException>(async () => await service.GetAccountInfoAsync(request));
                Assert.Equal(FirebaseAuthMessageType.InvalidIdToken, exception.Error?.MessageType);
            }
        }

        [Fact]
        public async Task GetAccountInfo_ValidIdToken_ReturnsAccountInfo()
        {
            using (var service = CreateService())
            {
                var signInRequset = new VerifyPasswordRequest()
                {
                    Email = knownValidEmail,
                    Password = knownValidPassword
                };

                var signInResponse = await service.VerifyPasswordAsync(signInRequset);

                var request = new GetAccountInfoRequest()
                {
                    IdToken = signInResponse.IdToken
                };

                var accountInfoResponse = await service.GetAccountInfoAsync(request);

                Assert.NotNull(accountInfoResponse);
                Assert.Equal(1, accountInfoResponse.Users.Count());
                Assert.Equal(knownValidEmail, accountInfoResponse.Users.FirstOrDefault().Email);

                // Has a "password" provider
                Assert.NotNull(accountInfoResponse.Users.Where(u => u.ProviderUserInfo.Any(x => x.ProviderId == "password")).FirstOrDefault());
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
            return new FirebaseAuthService(new FirebaseAuthOptions() { WebApiKey = webApiKey });
        }

        #endregion
    }
}