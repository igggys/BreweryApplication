namespace WebAppGeoCodingServices.Infrastructure
{
    public class ApplicationResponse
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
        public Guid? SessionId { get; set; }
        public object Data { get; set; }
    }
}
