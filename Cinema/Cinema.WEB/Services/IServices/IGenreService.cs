using Cinema.WEB.Models.GenreModels.GenreDtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cinema.WEB.Services.IServices
{
    public interface IGenreService
    {
        Task<IEnumerable<SelectListItem>> GetSelectListOfGenresAsync(string token);

        Task<List<GenreDto>> GetAllGenersAsync(string token);

        Task<GenreDto> GetGenreAsync(Guid id, string token);

        Task<bool> CreateGenreAsync(GenreCreateDto createDto, string token);

        Task<bool> UpdateGenreAsync(GenreDto genreDto, string token);

        Task<bool> DeleteGenreAsync(Guid id, string token);
    }
}
