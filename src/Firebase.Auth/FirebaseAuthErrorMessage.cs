using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Firebase.Auth
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FirebaseAuthMessageType
    {
        Unknown,
        [EnumMember(Value = "OPERATION_NOT_ALLOWED")]
        OperationNotAllowed,
        [EnumMember(Value = "EMAIL_EXISTS")]
        EmailExists,
        [EnumMember(Value = "WEAK_PASSWORD")]
        WeakPassword,
        [EnumMember(Value = "MISSING_PASSWORD")]
        MissingPassword,
        [EnumMember(Value = "INVALID_EMAIL")]
        InvalidEmail,
        [EnumMember(Value = "MISSING_EMAIL")]
        MissingEmail
    }
}
