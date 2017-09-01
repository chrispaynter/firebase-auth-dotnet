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
        /// Exchanges a refresh token for a new Id token with a renewed expiry.
        /// </summary>
        Task<ExchangeRefreshTokenResponse> ExchangeRefreshToken(ExchangeRefreshTokenRequest request);

        /// <summary>
        /// Creates a new user in Firebase.
        /// </summary>
        Task<SignUpNewUserResponse> SignUpNewUserAsync(SignUpNewUserRequest request);

        /// <summary>
        /// Verifies the password for a given user. This is equivalent to signing the user in
        /// with an email and password.
        /// </summary>
        Task<VerifyPasswordResponse> VerifyPasswordAsync(VerifyPasswordRequest request);
    }
}
