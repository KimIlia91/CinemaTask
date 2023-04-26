using Azure;
using Cinema.API.Models.MovieModels.MovieDtos;

namespace Cinema.API.Models.MovieModels
{
    public class MoviesFilteredResponse
    {
        public string? Search { get; set; }

        public string? TitleFilter { get; set; }

        public string? DirectorFilter { get; set; }

        public string? GenreFilter { get; set; }

        public bool Sort { get; set; } = true;

        public Page<MovieDto> MoviesPage { get; set; } = null!;
    }
}
