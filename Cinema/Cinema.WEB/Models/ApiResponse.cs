using System.Net;

namespace Cinema.WEB.Models
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccess { get; set; }

        public List<string> ErrorMessage { get; set; } = new List<string>();

        public object? Result { get; set; }
    }
}
