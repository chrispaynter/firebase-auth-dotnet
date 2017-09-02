using Jose;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.IO;

namespace Firebase.Auth.Tokens
{
    /// <summary>
    /// Generates custom tokens to use with Firebase Auth API. You should make this class a singleton.
    /// https://stackoverflow.com/questions/38188122/firebase-3-creating-a-custom-authentication-token-using-net-and-c-sharp
    /// </summary>
    public class FirebaseTokenGenerator
    {
        // private_key from the Service Account JSON file
        public string firebasePrivateKey;

        // Same for everyone
        public string firebasePayloadAUD = "https://identitytoolkit.googleapis.com/google.identity.identitytoolkit.v1.IdentityToolkit";

        // client_email from the Service Account JSON file
        public string firebasePayloadISS;
        public string firebasePayloadSUB;

        // the token 'exp' - max 3600 seconds - see https://firebase.google.com/docs/auth/server/create-custom-tokens
        public int firebaseTokenExpirySecs = 3600;

        private RsaPrivateCrtKeyParameters _rsaParams;
        private object _rsaParamsLocker = new object();

        public FirebaseTokenGenerator(string privateKey, string clientEmail)
        {
            firebasePrivateKey = privateKey ?? throw new ArgumentNullException(nameof(privateKey));
            firebasePayloadISS = clientEmail ?? throw new ArgumentNullException(nameof(clientEmail));
            firebasePayloadSUB = clientEmail;
        }

        public string EncodeToken(string uid)
        {
            return EncodeToken(uid, null);
        }

        public string EncodeToken(string uid, Dictionary<string, object> claims)
        {
            // Get the RsaPrivateCrtKeyParameters if we haven't already determined them
            if (_rsaParams == null)
            {
                lock (_rsaParamsLocker)
                {
                    if (_rsaParams == null)
                    {
                        using (var streamWriter = WriteToStreamWithString(firebasePrivateKey.Replace(@"\n", "\n")))
                        {
                            using (var sr = new StreamReader(streamWriter.BaseStream))
                            {
                                var pemReader = new Org.BouncyCastle.OpenSsl.PemReader(sr);

                                _rsaParams = (RsaPrivateCrtKeyParameters)pemReader.ReadObject();

                                if (_rsaParams == null)
                                {
                                    throw new FirebaseTokenException("The private key could not be read. Is the format valid?");
                                }
                            }
                        }
                    }
                }
            }

            var payload = new Dictionary<string, object>
            {
                { "uid", uid},
                { "iat", SecondsSinceEpoch(DateTime.UtcNow)},
                { "exp", SecondsSinceEpoch(DateTime.UtcNow.AddSeconds(firebaseTokenExpirySecs))},
                { "aud", firebasePayloadAUD},
                { "iss", firebasePayloadISS},
                { "sub", firebasePayloadSUB}
            };

            if (claims != null && claims.Count > 0)
            {
                payload.Add("claims", claims);
            }

            return JWT.Encode(payload, Org.BouncyCastle.Security.DotNetUtilities.ToRSA(_rsaParams), JwsAlgorithm.RS256);
        }


        private static long SecondsSinceEpoch(DateTime dt)
        {
            TimeSpan t = dt - new DateTime(1970, 1, 1);
            return (long)t.TotalSeconds;
        }

        private static StreamWriter WriteToStreamWithString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return writer;
        }
    }
}
