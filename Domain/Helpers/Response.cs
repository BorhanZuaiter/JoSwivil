namespace Domain.Helpers
{
    public class Response
    {
        public bool Status { get; protected set; }
        public string Message { get; protected set; }
        public static Response Success(string message = "")
        {
            return new Response
            {
                Status = true,
                Message = message
            };
        }
        public static Response Failure(string message)
        {
            return new Response
            {
                Status = false,
                Message = message
            };
        }
    }
}