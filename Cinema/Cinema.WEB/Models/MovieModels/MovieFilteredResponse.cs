using Cinema.WEB.Models.MovieModels.MovieVms;

namespace Cinema.WEB.Models.MovieModels
{
    public class MovieFilteredResponse
    {
        public string? Search { get; set; }

        public string? TitleFilter { get; set; }

        public string? DirectorFilter { get; set; }

        public string? GenreFilter { get; set; }

        public bool Sort { get; set; } = true;

        public PageResponse<MovieVm> MoviesPage { get; set; } = null!;
    }
}
