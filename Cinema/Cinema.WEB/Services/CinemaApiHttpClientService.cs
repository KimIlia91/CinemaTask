using Cinema.WEB.Models;
using Cinema.WEB.Services.IServices;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Cinema.WEB.Services
{
    public class CinemaApiHttpClientService : ICinemaApiHttpClientService
    {
        private readonly HttpClient _client;

        public IHttpClientFactory HttpClient { get; set; }

        public CinemaApiHttpClientService(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            HttpClient = httpClient;
            _client = HttpClient.CreateClient("CinemaApi");
            _client.BaseAddress = new Uri(configuration.GetValue<string>("ServiceUrl:CinemaApi")!);
        }

        public async Task<ApiResponse> GetAsync(ApiRequest request)
        {
            try
            {
                _client.DefaultRequestHeaders.Clear();
                if (!string.IsNullOrEmpty(request.Token))
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.Token);

                var response = await _client.GetFromJsonAsync<ApiResponse>(request.Url);
                return response!;
            }
            catch (Exception ex)
            {
                return new ApiResponse() { ErrorMessage = new List<string> { ex.Message } };
            }
        }

        public async Task<ApiResponse> PostAsync(ApiRequest request)
        {
            try
            {
                _client.DefaultRequestHeaders.Clear();
                if (!string.IsNullOrEmpty(request.Token))
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.Token);

                var strContent = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(request.Url, strContent);
                var strResponse = await response.Content.ReadAsStringAsync();
                var convertResponse = JsonConvert.DeserializeObject<ApiResponse>(strResponse);
                return convertResponse!;
            }
            catch (Exception ex)
            {
                return new ApiResponse() { ErrorMessage = new List<string> { ex.Message } };
            }
        }

        public async Task<ApiResponse> DeleteAsync(ApiRequest request)
        {
            try
            {
                _client.DefaultRequestHeaders.Clear();
                if (!string.IsNullOrEmpty(request.Token))
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.Token);

                var response = await _client.DeleteFromJsonAsync<ApiResponse>(request.Url);
                return response!;
            }
            catch (Exception ex)
            {
                return new ApiResponse() { ErrorMessage = new List<string> { ex.Message } };
            }
        }

        public async Task<ApiResponse> PutAsync(ApiRequest request)
        {
            try
            {
                _client.DefaultRequestHeaders.Clear();
                if (!string.IsNullOrEmpty(request.Token))
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.Token);

                var strContent = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");
                var response = await _client.PutAsync(request.Url, strContent);
                var strResponse = await response.Content.ReadAsStringAsync();
                var convertResponse = JsonConvert.DeserializeObject<ApiResponse>(strResponse);
                return convertResponse!;
            }
            catch (Exception ex)
            {
                return new ApiResponse() { ErrorMessage = new List<string> { ex.Message } };
            }
        }
    }
}
