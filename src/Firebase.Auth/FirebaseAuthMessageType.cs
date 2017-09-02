using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Firebase.Auth
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FirebaseAuthMessageType
    {
        /// <summary>
        /// The error message being deserialized from Firebase is of type unknown.
        /// </summary>
        Unknown,

        /// <summary>
        /// Please pass a valid API key. (invalid API key provided)
        /// </summary>
        [EnumMember(Value = "API key not valid")]
        ApiKeyNotValid,

        /// <summary>
        /// This error code differs in meaning depending on endpoint being called. Checkout the official Firebase Rest API docs for clarification.
        /// </summary>
        [EnumMember(Value = "OPERATION_NOT_ALLOWED")]
        OperationNotAllowed,

        /// <summary>
        /// The email address is already in use by another account.
        /// </summary>
        [EnumMember(Value = "EMAIL_EXISTS")]
        EmailExists,

        /// <summary>
        /// The password must be 6 characters long or more.
        /// </summary>
        [EnumMember(Value = "WEAK_PASSWORD")]
        WeakPassword,

        /// <summary>
        /// Password is required
        /// </summary>
        [EnumMember(Value = "MISSING_PASSWORD")]
        MissingPassword,

        /// <summary>
        /// The password is invalid or the user does not have a password.
        /// </summary>
        [EnumMember(Value = "INVALID_PASSWORD")]
        InvalidPassword,

        /// <summary>
        /// The email address is badly formatted.
        /// </summary>
        [EnumMember(Value = "INVALID_EMAIL")]
        InvalidEmail,

        /// <summary>
        /// Email is required
        /// </summary>
        [EnumMember(Value = "MISSING_EMAIL")]
        MissingEmail,

        /// <summary>
        /// There is no user record corresponding to this identifier. The user may have been deleted.
        /// </summary>
        [EnumMember(Value = "EMAIL_NOT_FOUND")]
        EmailNotFound,

        /// <summary>
        ///  The user corresponding to this request was not found. It is likely the user was deleted.
        /// </summary>
        [EnumMember(Value = "USER_NOT_FOUND")]
        UserNotFound,
        

        /// <summary>
        /// The user account has been disabled by an administrator.
        /// </summary>
        [EnumMember(Value = "USER_DISABLED")]
        UserDisabled,

        /// <summary>
        /// The user's credential is no longer valid. The user must sign in again.
        /// </summary>
        [EnumMember(Value = "TOKEN_EXPIRED")]
        TokenExpired,

        /// <summary>
        /// An invalid refresh token is provided.
        /// </summary>
        [EnumMember(Value = "INVALID_REFRESH_TOKEN")]
        InvalidRefreshToken,

        /// <summary>
        /// The grant type specified is invalid. Generally only occurs on the securetoken Google API url.
        /// </summary>
        [EnumMember(Value = "INVALID_GRANT_TYPE")]
        InvalidGrantType,

        /// <summary>
        /// The grant type specified is invalid. Generally only occurs on the securetoken Google API url.
        /// </summary>
        [EnumMember(Value = "MISSING_REFRESH_TOKEN")]
        MissingRefreshToken,

        /// <summary>
        /// The custom token format is incorrect or the token is invalid for some reason (e.g. expired, invalid signature etc.)
        /// </summary>
        [EnumMember(Value = "INVALID_CUSTOM_TOKEN")]
        InvalidCustomToken,

        /// <summary>
        /// The custom token corresponds to a different Firebase project.
        /// </summary>
        [EnumMember(Value = "CREDENTIAL_MISMATCH")]
        CredentialMismatch

    }
}
