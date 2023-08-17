namespace OrderProcessing.Application.Responses
{
    public class ErrorResponse
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Details { get; set; }
    }
}