namespace dotta.Net
{
    public class DottaResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        public DottaResponse(bool status, string message)
        {
            Status = status;
            Message = message;
        }
    }

    public class DottaResponse<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public DottaResponse(bool status, string message, T data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }
}
