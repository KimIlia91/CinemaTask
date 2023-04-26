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

        public UnitOfWork(IConfiguration configuration, IHttpClientFactory httpClient, IWebHostEnvironment hostEnvironment)
        {
            Movies = new MovieService(httpClient, configuration);
            Persons = new PersonService(httpClient, configuration);
            Genres = new GenreService(httpClient, configuration);
            Actors = new ActorService(httpClient, configuration);
            Directors = new DirectorService(httpClient, configuration);
            Screenwriters = new ScreenwriterService(httpClient, configuration);
            Images = new ImageService(hostEnvironment);
            Videos = new VideoService(hostEnvironment);
        }
    }
}
