using Cinema.API.Models.CastModels;

namespace Cinema.API.Repositories.IRepositories
{
    public interface IScreenwriterRepository : IBaseRepository<Screenwriter>
    {
        Task<List<Screenwriter>> GetScreenwritersByIdsAsync(List<Guid> screenwriterIds, string? includeProperty = null);
    }
}
