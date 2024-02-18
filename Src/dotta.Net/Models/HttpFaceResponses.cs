using System;

namespace dotta.Net
{
    public class HttpDottaFaceAttributesResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public HttpFaceAttributesResponse data { get; set; }
    }
    public class HttpFaceAttributesResponse
    {
        public int? errorCode { get; set; }
        public string errorMessage { get; set; }
        public float male { get; set; }
        public float female { get; set; }
        public float age { get; set; }
        public float smile { get; set; }
        public float eyesOpen { get; set; }
        public float passiveLiveness { get; set; }
        public string orientation { get; set; }
    }

}
