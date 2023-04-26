using Cinema.WEB.Helpers;
using Cinema.WEB.Models;
using Cinema.WEB.Services.IServices;
using Newtonsoft.Json;
using System.Text;

namespace Cinema.WEB.Services
{
    public class BaseService : IBaseService
    {
        private readonly HttpClient client;
        private bool disposedValue = false;

        public ApiResponse ResponceModel { get; set; }

        public IHttpClientFactory HttpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            ResponceModel = new();
            HttpClient = httpClient;
            client = HttpClient.CreateClient("CinemaApi");
        }

        public async Task<ApiResponse> SendAsync(ApiRequest apiRequest)
        {
            try
            {
                var message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                client.DefaultRequestHeaders.Clear();

                if (apiRequest.Data != null)
                    message.Content = new StringContent(
                        JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");

                HttpResponseMessage? apiResponse = null;
                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case SD.ApiType.GET:
                        message.Method = HttpMethod.Get;
                        break;
                }
                apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var apiResponseDto = JsonConvert.DeserializeObject<ApiResponse>(apiContent);
                return apiResponseDto!;
            }
            catch (Exception ex)
            {
                var dto = new ApiResponse
                {
                    ErrorMessage = new List<string> { Convert.ToString(ex.Message) }
                };

                var res = JsonConvert.SerializeObject(dto);
                var apiResponseDto = JsonConvert.DeserializeObject<ApiResponse>(res);
                return apiResponseDto!;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BaseService()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    client.Dispose();
                }

                disposedValue = true;
            }
        }
    }
}
