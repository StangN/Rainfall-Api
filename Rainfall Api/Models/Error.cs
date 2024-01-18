namespace Rainfall_Api.Models
{
    public class Error
    {
        public string Message { get; set; }

        public ErrorDetail[] Details { get; set; }
    }

    public class ErrorDetail
    {
        public string PropertyName { get; set; }

        public string Message { get; set; }
    }
}
