namespace Cinema.WEB.Models.MovieModels
{
    public class MoviesFilterRequest
    {
        public string? Search { get; set; }

        public string? TitleFilter { get; set; }

        public string? DirectorFilter { get; set; }

        public string? GenreFilter { get; set; }

        public bool Sort { get; set; }

        public int PageSize { get; set; }

        public int Page { get; set; }

        public MoviesFilterRequest(string? search = null,
                                  string? titleFilter = null,
                                  string? directorFilter = null,
                                  string? genreFilter = null,
                                  bool sort = false,
                                  int pageSize = 20,
                                  int page = 1)
        {
            Search = search;
            TitleFilter = titleFilter;
            DirectorFilter = directorFilter;
            GenreFilter = genreFilter;
            Sort = sort;
            PageSize = pageSize;
            Page = page;
        }
    }
}
