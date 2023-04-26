using Cinema.API.Models.GenreModels.GenreDtos;

namespace Cinema.API.Services.IServices
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetAllGenresAsync();

        Task CreateGenreAsync(GenreCreateDto genreCreateDto);

        Task<GenreDto> GetGenreByIdAsync(Guid id);

        Task UpdateGenreAsync(GenreDto genreDto);

        Task DeleteGenreByIdAsync(Guid id);
    }
}
