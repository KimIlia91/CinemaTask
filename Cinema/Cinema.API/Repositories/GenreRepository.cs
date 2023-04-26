using Cinema.API.Data;
using Cinema.API.Models.GenreModels;
using Cinema.API.Repositories.IRepositories;

namespace Cinema.API.Repositories
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
