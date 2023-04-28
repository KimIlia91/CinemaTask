using Cinema.WEB.Models;

namespace Cinema.WEB.Services.IServices
{
    public interface ICinemaApiHttpClientService
    {
        Task<ApiResponse> GetAsync(ApiRequest request);

        Task<ApiResponse> PostAsync(ApiRequest request);

        Task<ApiResponse> PutAsync(ApiRequest request);

        Task<ApiResponse> DeleteAsync(ApiRequest request);
    }
}
