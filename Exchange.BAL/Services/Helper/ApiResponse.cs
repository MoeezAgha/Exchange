namespace Exchange.Library.Helper
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; } // Optional, for additional info or error messages

        public System.Net.HttpStatusCode statusCode { get; set; }
    }
}
