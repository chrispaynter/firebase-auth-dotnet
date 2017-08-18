using Firebase.Auth.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Firebase.Auth.Payloads
{
    public class FirebaseAuthErrorResponseWrapper
    {
        public FirebaseAuthErrorResponse Error { get; set; }
    }

    public class FirebaseAuthErrorResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public IEnumerable<FirebaseAuthError> Errors { get; set; }


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
