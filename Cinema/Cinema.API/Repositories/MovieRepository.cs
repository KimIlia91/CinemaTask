using Cinema.API.Data;
using Cinema.API.Models.MovieModels;
using Cinema.API.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Cinema.API.Repositories
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        private readonly ApplicationDbContext _db;

        public MovieRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IQueryable<Movie> GetFilteredMoviesQuery(
            string? search, string? titleFilter, string? directorFilter, string? genreFilter)
        {
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return _db.Movies.Include(m => m.Directors)
                                .ThenInclude(d => d.Person)
                             .Include(m => m.Screenwriters)
                                .ThenInclude(d => d.Person)
                             .Include(m => m.Actors)
                                .ThenInclude(d => d.Person)
                             .Include(m => m.Genre)
                             .Where(m => (string.IsNullOrEmpty(search) ||
                                               m.Title.Contains(search) ||
                                               m.ShortDescription.Contains(search) ||
                                               m.Description.Contains(search) ||
                                               m.Actors.Any(a => a.Person.FirstName.Contains(search) ||
                                                                 a.Person.LastName.Contains(search)) ||
                                               m.Directors.Any(d => d.Person.FirstName.Contains(search) ||
                                                                    d.Person.LastName.Contains(search)) ||
                                               m.Genre.Name.Contains(search)) &&
                                         (string.IsNullOrEmpty(titleFilter) || m.Title == titleFilter) &&
                                         (string.IsNullOrEmpty(directorFilter) || m.Directors.Any(d => d.Person.FirstName == directorFilter ||
                                                                                                       d.Person.LastName == directorFilter)) &&
                                         (string.IsNullOrEmpty(genreFilter) || m.Genre.Name == genreFilter));
        }

        public IQueryable<Movie> GetSortedMoviesQuery(IQueryable<Movie> query, bool sort = true) =>
            sort ? query.OrderByDescending(m => m.Title) : query.OrderBy(m => m.PublicationDate);

        public async Task<Movie?> GetMovieIncludePersonAsync(Guid movieId)
        {
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var movie = await _db.Movies.Include(m => m.Actors)
                                            .ThenInclude(a => a.Person)
                                        .Include(m => m.Directors)
                                            .ThenInclude(d => d.Person)
                                        .Include(m => m.Screenwriters)
                                            .ThenInclude(s => s.Person)
                                        .Include(m => m.Genre)
                                        .FirstOrDefaultAsync(m => m.Id == movieId);
            return movie;
        }

        public async Task UpdateMovieAndSaveAsync(Movie newMovie)
        {
            var currentMovie = await _db.Movies.Include(m => m.Actors)
                                               .Include(m => m.Directors)
                                               .Include(m => m.Screenwriters)
                                               .FirstAsync(m => m.Id == newMovie.Id);

            _db.Movies.Entry(currentMovie).CurrentValues.SetValues(newMovie);
            await UpdateActorRelationshipsAsync(newMovie, currentMovie);
            await UpdateScreenwriterRelationshipsAsync(newMovie, currentMovie);
            await UpdateDirectorRelationshipsAsync(newMovie, currentMovie);
            await _db.SaveChangesAsync();
        }

        private async Task UpdateActorRelationshipsAsync(Movie newMovie, Movie currentMovie)
        {
            var currentActors = currentMovie.Actors.ToList();

            foreach (var currentActor in currentActors)
            {
                var newActor = newMovie.Actors.FirstOrDefault(a => a.Id == currentActor.Id);
                if (newActor is null)
                    currentMovie.Actors.Remove(currentActor);
            }

            foreach (var actor in newMovie.Actors)
            {
                var newActor = await _db.Actors.FirstOrDefaultAsync(a => a.Id == actor.Id);
                if (newActor != null && currentActors.All(i => i.Id != actor.Id))
                    currentMovie.Actors.Add(newActor);
            }
        }

        private async Task UpdateDirectorRelationshipsAsync(Movie newMovie, Movie currentMovie)
        {
            var currentDirectors = currentMovie.Directors.ToList();

            foreach (var currentDirector in currentDirectors)
            {
                var newDirector = newMovie.Directors.FirstOrDefault(a => a.Id == currentDirector.Id);
                if (newDirector is null)
                    currentMovie.Directors.Remove(currentDirector);
            }

            foreach (var director in newMovie.Directors)
            {
                var newDirector = await _db.Directors.FirstOrDefaultAsync(a => a.Id == director.Id);
                if (newDirector != null && currentDirectors.All(i => i.Id != director.Id))
                    currentMovie.Directors.Add(newDirector);
            }
        }

        private async Task UpdateScreenwriterRelationshipsAsync(Movie newMovie, Movie currentMovie)
        {
            var currentScreenwriters = currentMovie.Screenwriters.ToList();

            foreach (var currentScreenwriter in currentScreenwriters)
            {
                var newScreenwriter = newMovie.Screenwriters.FirstOrDefault(a => a.Id == currentScreenwriter.Id);
                if (newScreenwriter is null)
                    currentMovie.Screenwriters.Remove(currentScreenwriter);
            }

            foreach (var screenwriter in newMovie.Screenwriters)
            {
                var newScreenwriter = await _db.Screenwriters.FirstOrDefaultAsync(a => a.Id == screenwriter.Id);
                if (newScreenwriter != null && currentScreenwriters.All(i => i.Id != screenwriter.Id))
                    currentMovie.Screenwriters.Add(newScreenwriter);
            }
        }
    }
}
