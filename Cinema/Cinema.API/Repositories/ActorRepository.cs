using Cinema.API.Data;
using Cinema.API.Models.CastModels;
using Cinema.API.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Cinema.API.Repositories
{
    public class ActorRepository : BaseRepository<Actor>, IActorRepository
    {
        private readonly ApplicationDbContext _db;

        public ActorRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Actor>> GetActorsByIdsAsync(List<Guid> actorIds, string? includeProperty = null)
        {
            if (actorIds is null)
                throw new ArgumentNullException(nameof(actorIds), "Список ID актёров не может быть NULL.");

            var actorsQuery = _db.Actors.Where(a => actorIds.Any(id => id == a.Id))
                                        .AsQueryable();
            if (includeProperty != null)
                foreach (var includeProp in includeProperty.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    actorsQuery = actorsQuery.Include(includeProp);

            var actors = await actorsQuery.ToListAsync();
            if (actorIds.Count != actors.Count)
                throw new ArgumentOutOfRangeException(nameof(actorIds), "Один или несколько актёров не найдено.");

            return actors;
        }
    }
}
