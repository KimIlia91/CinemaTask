using Cinema.WEB.Helpers;
using Cinema.WEB.Models.MovieModels;
using Cinema.WEB.Models;
using Newtonsoft.Json;
using Cinema.WEB.Services.IServices;
using Cinema.WEB.Models.MovieModels.MovieDtos;

namespace Cinema.WEB.Services
{
    public class MovieService : IMovieService
    {
        private readonly ICinemaApiHttpClientService _cinemaApi;

        public MovieService(ICinemaApiHttpClientService cinemaApi)
        {
            _cinemaApi = cinemaApi;
        }

        public async Task<bool> CreateMovieAsync(MovieCreateDto dto, string token)
        {
            var response = await _cinemaApi.PostAsync(new ApiRequest()
            {
                Url = "/api/movie",
                Data = dto,
                Token = token
            });

            return response.IsSuccess;
        }

        public async Task<bool> DeleteMovieAsync(Guid id, string token)
        {
            var response = await _cinemaApi.DeleteAsync(new ApiRequest()
            {
                Url = "/api/movie/" + id,
                Token = token
            });

            return response.IsSuccess;
        }

        public async Task<MovieFilteredResponse> GetAllMoviesAsync(string token, MoviesFilterRequest opt)
        {
            var response = await _cinemaApi.GetAsync(new ApiRequest()
            {
                Url = $"/api/movie?search={opt.Search}&titleFilter={opt.TitleFilter}&DirectorFilter={opt.DirectorFilter}&GenreFilter={opt.GenreFilter}&sort={opt.Sort}&page={opt.Page}&pageSize={opt.PageSize}",
                Token = token
            });

            if (response.Result is not null && response.IsSuccess)
            {
                var page = JsonConvert.DeserializeObject<MovieFilteredResponse>(Convert.ToString(response.Result)!);
                return page!;
            }

            return new MovieFilteredResponse();
        }

        public async Task<MovieDto> GetMovieAsync(Guid? id, string token)
        {
            var response = await _cinemaApi.GetAsync(new ApiRequest()
            {
                Url = "/api/movie/" + id,
                Token = token
            });

            if (response.Result is not null && response.IsSuccess)
            {
                var movieDto = JsonConvert.DeserializeObject<MovieDto>(Convert.ToString(response.Result)!);
                return movieDto!;
            }

            return new MovieDto();
        }

        public async Task<bool> UpdateMovieAsync(MovieUpdateDto dto, string token)
        {
            var respnse = await _cinemaApi.PutAsync(new ApiRequest()
            {
                Url = "/api/movie",
                Data = dto,
                Token = token
            });

            return respnse.IsSuccess;
        }
    }
}
