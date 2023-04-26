using Cinema.API.Data;
using Cinema.API.Models.CastModels;
using Cinema.API.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Cinema.API.Repositories
{
    public class DirectorRepository : BaseRepository<Director>, IDirectorRepository
    {
        private readonly ApplicationDbContext _db;

        public DirectorRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Director>> GetDirectorsByIdsAsync(List<Guid> directorIds, string? includeProperty = null)
        {
            if (directorIds is null)
                throw new ArgumentNullException(nameof(directorIds), "Список ID режиссеров не может быть NULL.");

            var directorsQuery = _db.Directors.Where(d => directorIds.Any(id => d.Id == id))
                                              .AsQueryable();
            if (includeProperty != null)
                foreach (var includeProp in includeProperty.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    directorsQuery = directorsQuery.Include(includeProp);

            var directors = await directorsQuery.ToListAsync();
            if (directors.Count != directorIds.Count)
                throw new ArgumentOutOfRangeException(nameof(directorIds), "Один или несколько режиссеров не найдено.");

            return directors;
        }
    }
}
