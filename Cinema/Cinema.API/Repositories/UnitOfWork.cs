using Cinema.API.Data;
using Cinema.API.Repositories.IRepositories;

namespace Cinema.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Репозиторий для работы с персонами.
        /// </summary>
        public IPersonRepository Persons { get; private set; }

        /// <summary>
        /// Репозиторий для работы с актерами.
        /// </summary>
        public IActorRepository Actors { get; private set; }

        /// <summary>
        /// Репозиторий для работы с режиссерами.
        /// </summary>
        public IDirectorRepository Directors { get; private set; }

        /// <summary>
        /// Репозиторий для работы со сценаристами.
        /// </summary>
        public IScreenwriterRepository Screenwriters { get; private set; }

        /// <summary>
        /// Репозиторий для работы с фильмами.
        /// </summary>
        public IMovieRepository Movies { get; private set; }

        /// <summary>
        /// Репозиторий для работы с жанрами фильмов
        /// </summary>
        public IGenreRepository Genres { get; private set; }

        /// <summary>
        /// Создает новый экземпляр класса UnitOfWork с указанным контекстом базы данных.
        /// </summary>
        /// <param name="db">Контекст базы данных.</param>
        public UnitOfWork(ApplicationDbContext db)
        {
            Persons = new PersonRepository(db);
            Actors = new ActorRepository(db);
            Directors = new DirectorRepository(db);
            Screenwriters = new ScreenwriterRepository(db);
            Movies = new MovieRepository(db);
            Genres = new GenreRepository(db);
        }
    }
}
