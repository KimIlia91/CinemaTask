using Cinema.WEB.Models;
using Cinema.WEB.Models.GenreModels.GenreDtos;
using Cinema.WEB.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Cinema.WEB.Services
{
    public class GenreService : IGenreService
    {
        private readonly ICinemaApiHttpClientService _cinemaApi;

        public GenreService(ICinemaApiHttpClientService cinemaApi)
        {
            _cinemaApi = cinemaApi;
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListOfGenresAsync(string token)
        {
            var response = await _cinemaApi.GetAsync(new ApiRequest()
            {
                Url = "/api/genre",
                Token = token
            });

            if (response.Result is null && !response.IsSuccess) return Enumerable.Empty<SelectListItem>();

            var dtos = JsonConvert.DeserializeObject<List<GenreDto>>(Convert.ToString(response.Result)!);
            return dtos!.Select(g => new SelectListItem { Text = g.Name, Value = g.Id.ToString() });
        }

        public async Task<List<GenreDto>> GetAllGenersAsync(string token)
        {
            var response = await _cinemaApi.GetAsync(new ApiRequest()
            {
                Url = "/api/genre",
                Token = token
            });

            if (response.Result is null && !response.IsSuccess) return new List<GenreDto>();

            var genreDtos = JsonConvert.DeserializeObject<IEnumerable<GenreDto>>(Convert.ToString(response.Result)!);
            return genreDtos!.ToList();
        }

        public async Task<GenreDto> GetGenreAsync(Guid id, string token)
        {
            var response = await _cinemaApi.GetAsync(new ApiRequest()
            {
                Url = "/api/genre/" + id,
                Token = token
            });

            if (response.Result is null && !response.IsSuccess) return new GenreDto();

            var genreDto = JsonConvert.DeserializeObject<GenreDto>(Convert.ToString(response.Result)!);
            return genreDto!;
        }

        public async Task<bool> CreateGenreAsync(GenreCreateDto createDto, string token)
        {
            var response = await _cinemaApi.PostAsync(new ApiRequest()
            {
                Url = "/api/genre",
                Data = createDto,
                Token = token
            });

            return response.IsSuccess;
        }

        public async Task<bool> UpdateGenreAsync(GenreDto genreDto, string token)
        {
            var response = await _cinemaApi.PutAsync(new ApiRequest()
            {
                Url = "/api/genre",
                Data = genreDto,
                Token = token
            });

            return response.IsSuccess;
        }

        public async Task<bool> DeleteGenreAsync(Guid id, string token)
        {
            var response = await _cinemaApi.DeleteAsync(new ApiRequest()
            {
                Url = "/api/genre/" + id,
                Token = token
            });

            return response.IsSuccess;
        }
    }
}
