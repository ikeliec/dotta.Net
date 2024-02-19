using System.Net.Http;

namespace dotta.Net
{
    public class DottaOptions
    {
        public string ApiKey { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public DottaEnvironment Environment { get; set; }
        public string BaseUrlProduction { get; set; }
        public string BaseUrlSandbox { get; set; }

        public HttpClient HttpClient { get; set; }
    }
}
