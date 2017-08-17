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
    }
}
