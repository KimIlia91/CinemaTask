
namespace Cinema.WEB.Models
{
    public class ApiRequest
    {
        public string Url { get; set; } = null!;

        public object Data { get; set; } = null!;

        public string Token { get; set; } = null!;
    }
}
