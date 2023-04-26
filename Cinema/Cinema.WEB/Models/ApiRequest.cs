using System.Security.AccessControl;
using static Cinema.WEB.Helpers.SD;

namespace Cinema.WEB.Models
{
    public class ApiRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;

        public string Url { get; set; } = null!;

        public object Data { get; set; } = null!;

        public string Token { get; set; } = null!;
    }
}
