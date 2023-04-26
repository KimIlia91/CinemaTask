using Cinema.WEB.Helpers;
using Cinema.WEB.Models;
using Cinema.WEB.Models.GenreModels.GenreDtos;
using Cinema.WEB.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Cinema.WEB.Services
{
    public class GenreService : BaseService, IGenreService
    {
        private readonly string _cinemaUrl;

        public GenreService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _cinemaUrl = configuration.GetValue<string>("ServiceUrl:CinemaApi")!;
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListOfGenresAsync(string token)
        {
            var response = await SendAsync(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{_cinemaUrl}/api/genre",
                Token = token
            });

            if (response.Result != null && response.IsSuccess)
            {
                var dtos = JsonConvert.DeserializeObject<List<GenreDto>>(Convert.ToString(response.Result)!);
                return dtos!.Select(g => new SelectListItem { Text = g.Name, Value = g.Id.ToString() });
            }

            return Enumerable.Empty<SelectListItem>();
        }

        public async Task<List<GenreDto>> GetAllGenersAsync(string token)
        {
            var response = await SendAsync(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = _cinemaUrl + "/api/genre",
                Token = token
            });

            if (response.Result != null && response.IsSuccess)
            {
                var genreDtos = JsonConvert.DeserializeObject<IEnumerable<GenreDto>>(Convert.ToString(response.Result)!);
                return genreDtos!.ToList();
            }

            return new List<GenreDto>();
        }

        public async Task<GenreDto> GetGenreAsync(Guid id, string token)
        {
            var response = await SendAsync(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = _cinemaUrl + "/api/genre/" + id,
                Token = token
            });

            if (response.Result != null && response.IsSuccess)
            {
                var genreDto = JsonConvert.DeserializeObject<GenreDto>(Convert.ToString(response.Result)!);
                return genreDto ?? new GenreDto();
            }

            return new GenreDto();
        }

        public async Task<bool> CreateGenreAsync(GenreCreateDto createDto, string token)
        {
            var response = await SendAsync(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = _cinemaUrl + "/api/genre",
                Data = createDto,
                Token = token
            });

            return response.IsSuccess;
        }

        public async Task<bool> UpdateGenreAsync(GenreDto genreDto, string token)
        {
            var response = await SendAsync(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Url = _cinemaUrl + "/api/genre",
                Data = genreDto,
                Token = token
            });

            return response.IsSuccess;
        }

        public async Task<bool> DeleteGenreAsync(Guid id, string token)
        {
            var response = await SendAsync(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = _cinemaUrl + "/api/genre/" + id,
                Token = token
            });

            return response.IsSuccess;
        }
    }
}
