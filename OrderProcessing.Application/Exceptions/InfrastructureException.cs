namespace OrderProcessing.Application.Exceptions
{
    public class InfrastructureException : Exception
    {
        public InfrastructureException(string message, Exception ex) : base(message) { }
    }
}
