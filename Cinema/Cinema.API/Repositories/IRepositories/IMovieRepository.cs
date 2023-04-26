using Cinema.API.Models.MovieModels;

namespace Cinema.API.Repositories.IRepositories
{
    public interface IMovieRepository : IBaseRepository<Movie>
    {
        IQueryable<Movie> GetFilteredMoviesQuery(
         string? search, string? titleFilter, string? directorFilter, string? genreFilter);

        IQueryable<Movie> GetSortedMoviesQuery(IQueryable<Movie> query, bool sort = true);

        Task<Movie?> GetMovieIncludePersonAsync(Guid movieId);

        Task UpdateMovieAndSaveAsync(Movie newMovie);
    }
}
