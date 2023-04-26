namespace Cinema.API.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        IPersonRepository Persons { get; }

        IActorRepository Actors { get; }

        IDirectorRepository Directors { get; }

        IScreenwriterRepository Screenwriters { get; }

        IMovieRepository Movies { get; }

        IGenreRepository Genres { get; }
    }
}
