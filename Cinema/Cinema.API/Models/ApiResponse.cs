using System.ComponentModel;
using System.Net;

namespace Cinema.API.Models
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
    
        public bool IsSuccess { get; set; } = false;

        public List<string> ErrorMessage { get; set; } = new List<string>();

        public object? Result { get; set; }
    }
}
