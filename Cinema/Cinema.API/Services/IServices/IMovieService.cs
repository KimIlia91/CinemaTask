using Cinema.API.Models.MovieModels.MovieDtos;
using Cinema.API.Models.MovieModels;
using Cinema.API.Models;

namespace Cinema.API.Services.IServices
{
    public interface IMovieService
    {
        Task<Page<MovieDto>> GetFilteredPageOfMoviesAsync(MoviesFilterRequest opt);

        Task CreateMovieAsync(MovieCreateDto movieDto);

        Task<MovieDto> GetMovieByIdAsync(Guid id);

        Task UpdateMovieAsync(MovieUpdateDto movieDto);

        Task DeleteMovieByIdAsync(Guid id);
    }
}
