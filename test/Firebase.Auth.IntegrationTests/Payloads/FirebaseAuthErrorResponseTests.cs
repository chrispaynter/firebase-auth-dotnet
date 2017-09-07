using Firebase.Auth.Payloads;
using Xunit;

namespace Firebase.Auth.IntegrationTests.Payloads
{
    public class FirebaseAuthErrorResponseTests
    {
        public void Message_Test(string message, FirebaseAuthMessageType expectedType)
        {
            var error = new FirebaseAuthErrorResponse()
            {
                Message = message
            };

            Assert.Equal(expectedType, error.MessageType);
        }

        [Fact]
        public void Message_EmailExistsMessage_EmailExistsType()
        {
            Message_Test("EMAIL_EXISTS: The email address is already in use by another account.", FirebaseAuthMessageType.EmailExists);
        }

        [Fact]
        public void Message_EmailNotFoundMessage_EmailNotFoundType()
        {
            Message_Test("EMAIL_NOT_FOUND: There is no user record corresponding to this identifier. The user may have been deleted.", FirebaseAuthMessageType.EmailNotFound);
        }

        [Fact]
        public void Message_InvalidEmailMessage_InvalidEmailType()
        {
            Message_Test("INVALID_EMAIL: The email address is badly formatted.", FirebaseAuthMessageType.InvalidEmail);
        }

        [Fact]
        public void Message_InvalidPasswordMessage_InvalidPasswordType()
        {
            Message_Test("INVALID_PASSWORD: The password is invalid or the user does not have a password.", FirebaseAuthMessageType.InvalidPassword);
        }

        [Fact]
        public void Message_MissingEmailMessage_MissingEmailType()
        {
            Message_Test("MISSING_EMAIL", FirebaseAuthMessageType.MissingEmail);
        }

        [Fact]
        public void Message_MissingPasswordMessage_MissingPasswordType()
        {
            Message_Test("MISSING_PASSWORD", FirebaseAuthMessageType.MissingPassword);
        }

        [Fact]
        public void Message_OperationNotAllowedmessage_OperationNotAllowedType()
        {
            Message_Test("OPERATION_NOT_ALLOWED: Password sign-in is disabled for this project.", FirebaseAuthMessageType.OperationNotAllowed);
        }

        [Fact]
        public void Message_UserDisabledmessage_UserDisabledType()
        {
            Message_Test("USER_DISABLED: The user account has been disabled by an administrator.", FirebaseAuthMessageType.UserDisabled);
        }

        [Fact]
        public void Message_WeakPasswordmessage_WeakPasswordType()
        {
            Message_Test("WEAK_PASSWORD: The password must be 6 characters long or more.", FirebaseAuthMessageType.WeakPassword);
        }

        [Fact]
        public void Message_InvalidIdTokenMessage_InvalidIdTokenType()
        {
            Message_Test("INVALID_ID_TOKEN:The user's credential is no longer valid. The user must sign in again.", FirebaseAuthMessageType.InvalidIdToken);
        }
    }
}
