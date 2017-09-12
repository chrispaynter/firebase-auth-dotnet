using Firebase.Auth.Helpers;
using Xunit;

namespace Firebase.Auth.IntegrationTests.Helpers
{
    public class EnumHelperTests
    {
        [Fact]
        public void GetValueIfStringStartsWith_StringIsNull_ReturnsDefault()
        {
            string str = null;
            var enumValue = EnumHelper.GetValueIfStringStartsWith<FirebaseAuthMessageType>(str);
            Assert.Equal(FirebaseAuthMessageType.Unknown, enumValue);
        }

        [Fact]
        public void GetValueIfStringStartsWith_DoesStartWith_ReturnsCorrectValue()
        {
            var str = "MISSING_PASSWORD : Password is required";
            var enumValue = EnumHelper.GetValueIfStringStartsWith<FirebaseAuthMessageType>(str);
            Assert.Equal(FirebaseAuthMessageType.MissingPassword, enumValue);
        }

        [Fact]
        public void GetValueIfStringStartsWith_DoesNotStartWith_ReturnsUnknown()
        {
            var str = "AN_ERROR_NOT_CAPTURED_YET : What is this?";
            var enumValue = EnumHelper.GetValueIfStringStartsWith<FirebaseAuthMessageType>(str);
            Assert.Equal(FirebaseAuthMessageType.Unknown, enumValue);
        }
    }
}
