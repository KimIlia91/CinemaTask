using Cinema.API.Data;
using Cinema.API.Models.CastModels;
using Cinema.API.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Cinema.API.Repositories
{
    public class ScreenwriterRepository : BaseRepository<Screenwriter>, IScreenwriterRepository
    {
        private readonly ApplicationDbContext _db;

        public ScreenwriterRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Screenwriter>> GetScreenwritersByIdsAsync(List<Guid> screenwriterIds, string? includeProperty = null)
        {
            if (screenwriterIds is null)
                throw new ArgumentNullException(nameof(screenwriterIds), "Список ID сценаристов не может быть NULL.");

            var screenwritersQuery = _db.Screenwriters.Where(s => screenwriterIds.Any(id => s.Id == id))
                                                      .AsQueryable();
            if (includeProperty != null)
                foreach (var includeProp in includeProperty.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    screenwritersQuery = screenwritersQuery.Include(includeProp);

            var screenwriters = await screenwritersQuery.ToListAsync();
            if (screenwriters.Count != screenwriterIds.Count)
                throw new ArgumentOutOfRangeException(nameof(screenwriterIds), "Один или несколько сценаристов не найдено.");

            return screenwriters;
        }
    }
}
