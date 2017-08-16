using Firebase.Auth.Helpers;
using Newtonsoft.Json;

namespace Firebase.Auth
{
    public class FirebaseAuthError
    {
        public string Domain { get; set; }
        public string Reason { get; set; }
        public string Message { get; set; }

        private FirebaseAuthMessageType _messageType;

        [JsonIgnore]
        public FirebaseAuthMessageType MessageType
        {
            get
            {
                if (_messageType == FirebaseAuthMessageType.Unknown)
                {
                    _messageType = EnumHelper.GetValueIfStringStartsWith<FirebaseAuthMessageType>(Message);
                }

                return _messageType;
            }
        }
    }
}
