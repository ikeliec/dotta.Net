namespace dotta.Net
{
    public class DottaResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class DottaResponse<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
