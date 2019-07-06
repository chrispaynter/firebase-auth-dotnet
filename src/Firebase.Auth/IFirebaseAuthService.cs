using Firebase.Auth.Payloads;
using System.Threading.Tasks;

namespace Firebase.Auth
{
    /// <summary>
    /// Service interface for connecting and communicating with the Firebase Auth REST API
    /// </summary>
    public interface IFirebaseAuthService
    {
        /// <summary>
        /// Creates a new user in Firebase.
        /// </summary>
        Task<SignUpNewUserResponse> SignUpNewUser(SignUpNewUserRequest request);

        /// <summary>
        /// Verifies the password for a given user. This is equivalent to signing the user in
        /// with an email and password.
        /// </summary>
        Task<VerifyPasswordResponse> VerifyPassword(VerifyPasswordRequest request);

        /// <summary>
        /// Verifies the user via refresh token.
        /// </summary>
        Task<VerifyRefreshTokenResponse> VerifyRefreshToken(VerifyRefreshTokenRequest request);

        /// <summary>
        /// Sends a verification email to the provided email address
        /// </summary>
        Task<SendVerificationEmailResponse> SendVerification(SendVerificationEmailRequest request);

        /// <summary>
        /// Sends a password change request to the provided user (id token)
        /// </summary>
        Task<ChangePasswordResponse> PasswordChange(ChangePasswordRequest request);

        /// <summary>
        /// Gets the data of a specific account
        /// </summary>
        Task<GetUserDataResponse> GetUserData(GetUserDataRequest request);
    }
}
