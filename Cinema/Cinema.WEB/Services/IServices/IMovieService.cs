using Cinema.WEB.Models;
using Cinema.WEB.Models.MovieModels;
using Cinema.WEB.Models.MovieModels.MovieDtos;

namespace Cinema.WEB.Services.IServices
{
    public interface IMovieService
    {
        Task<MovieFilteredResponse> GetAllMoviesAsync(string token, MoviesFilterRequest opt);

        Task<MovieDto> GetMovieAsync(Guid? id, string token);

        Task<ApiResponse> CreateMovieAsync(MovieCreateDto dto, string token);

        Task<ApiResponse> UpdateMovieAsync(MovieUpdateDto dto, string token);

        Task<bool> DeleteMovieAsync(Guid id, string token);
    }
}
