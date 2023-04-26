using Cinema.API.Models.CastModels;

namespace Cinema.API.Repositories.IRepositories
{
    public interface IActorRepository : IBaseRepository<Actor>
    {
        Task<List<Actor>> GetActorsByIdsAsync(List<Guid> actorIds, string? includeProperty = null);
    }
}
