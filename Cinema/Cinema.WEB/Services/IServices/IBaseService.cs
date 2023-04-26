using Cinema.WEB.Models;

namespace Cinema.WEB.Services.IServices
{
    public interface IBaseService : IDisposable
    {
        ApiResponse ResponceModel { get; set; }

        Task<ApiResponse> SendAsync(ApiRequest apiRequest);
    }
}
