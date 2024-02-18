using System;
using System.Text;

namespace dotta.Net
{
    public class Dotta
    {
        public string ApiKey { get; }
        public DottaEnvironment Environment { get; }

        /// <summary>
        /// Instantiate the dotta.Net object and pass your base64 encoded dotta PUBLICKEY:PRIVATEKEY and the dotta environment you want to use as arguements.
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="environment"></param>
        public Dotta(string apiKey, DottaEnvironment environment)
        {
            ApiKey = apiKey;
            Environment = environment;
        }

        /// <summary>
        /// Instantiate the dotta.Net object and pass your dotta public key, private key, and environment you want to use as arguements.
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="privateKey"></param>
        /// <param name="environment"></param>
        public Dotta(string publicKey, string privateKey, DottaEnvironment environment)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes($"{publicKey}:{privateKey}");
            var base64String = Convert.ToBase64String(plainTextBytes);

            ApiKey = base64String;
            Environment = environment;
        }

        public DottaResponse FaceDetection()
        {
            throw new NotImplementedException();
        }

        public DottaResponse FaceMatch()
        {
            throw new NotImplementedException();
        }

        public DottaResponse LivenessCheck()
        {
            throw new NotImplementedException();
        }

        public DottaResponse FaceAttributes()
        {
            throw new NotImplementedException();
        }
    }
}
