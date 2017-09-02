using Firebase.Test;
using System.Collections.Generic;
using Xunit;

namespace Firebase.Auth.Tokens.Tests
{
    public class FirebaseTokenGeneratorTests
    {

        [Fact]
        public void EncodeToken_WithClaims_ValidJwtOutput()
        {
            var section = SharedTestConfiguration.Configuration.GetSection("adminContext");
            var firebasePrivateKey = section["private_key"];
            var clientEmail = section["client_email"];

            var tokenGenerator = new FirebaseTokenGenerator(firebasePrivateKey, clientEmail);

            var uid = "myuserid";
            var claims = new Dictionary<string, object> {
                {"premium_account", true}
            };

            var result = tokenGenerator.EncodeToken(uid, claims);
        }

        [Fact]
        public void EncodeToken_WithoutClaims_ValidJwtOutput()
        {
            var section = SharedTestConfiguration.Configuration.GetSection("adminContext");
            var firebasePrivateKey = section["private_key"];
            var clientEmail = section["client_email"];

            var tokenGenerator = new FirebaseTokenGenerator(firebasePrivateKey, clientEmail);

            var uid = "myuserid";

            var result = tokenGenerator.EncodeToken(uid);
        }

        [Fact]
        public void EncodeToken_MalformedPrivateKey_ThrowsFirebaseTokenException()
        {
            var tokenGenerator = new FirebaseTokenGenerator("Invalid Key", "valueDoesNotMatter");

            var uid = "myuserid";

            var exception = Assert.Throws<FirebaseTokenException>(() => tokenGenerator.EncodeToken(uid));
            Assert.Equal("The private key could not be read. Is the format valid?", exception.Message);
        }
    }
}
