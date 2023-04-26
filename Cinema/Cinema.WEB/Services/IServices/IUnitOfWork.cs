namespace Cinema.WEB.Services.IServices
{
    public interface IUnitOfWork
    {
        IMovieService Movies { get; }

        IPersonService Persons { get; }

        IGenreService Genres { get; }

        IActorService Actors { get; }

        IDirectorService Directors { get; }

        IScreenwriterService Screenwriters { get; }

        IImageService Images { get; }

        IVideoService Videos { get; }
    }
}
