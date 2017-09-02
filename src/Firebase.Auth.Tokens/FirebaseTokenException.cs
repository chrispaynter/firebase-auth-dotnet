using System;
using System.Runtime.Serialization;

namespace Firebase.Auth.Tokens
{
    public class FirebaseTokenException : Exception
    {
        public FirebaseTokenException()
        {
        }

        public FirebaseTokenException(string message) : base(message)
        {
        }

        public FirebaseTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FirebaseTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
