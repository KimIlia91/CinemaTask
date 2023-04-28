using Cinema.WEB.Services.IServices;

namespace Cinema.WEB.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        public IMovieService Movies { get; private set; }

        public IPersonService Persons { get; private set; }

        public IGenreService Genres { get; private set; }

        public IActorService Actors { get; private set; }

        public IDirectorService Directors { get; private set; }

        public IScreenwriterService Screenwriters { get; set; }

        public IImageService Images { get; private set; }

        public IVideoService Videos { get; private set; }

        public UnitOfWork(IWebHostEnvironment hostEnvironment, ICinemaApiHttpClientService clientService)
        {
            Movies = new MovieService(clientService);
            Persons = new PersonService(clientService);
            Genres = new GenreService(clientService);
            Actors = new ActorService(clientService);
            Directors = new DirectorService(clientService);
            Screenwriters = new ScreenwriterService(clientService);
            Images = new ImageService(hostEnvironment);
            Videos = new VideoService(hostEnvironment);
        }
    }
}
